using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Text.Json;
using WeatherAPI.Shared.Models;

/// <summary>
/// Main class for the console application that interacts with a weather API.
/// </summary>
public class Program
{
    private static readonly HttpClient client = new HttpClient();
    private static IConfiguration? _configuration;

    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command-line arguments.</param>
    public static async Task Main(string[] args)
    {
        var builder = new HostBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })
            .ConfigureServices((hostingContext, services) =>
            {
                _configuration = hostingContext.Configuration;
            }).UseConsoleLifetime();

        var host = builder.Build();
        using (host)
        {
            await host.StartAsync();

            await RunApplicationAsync();

            await host.StopAsync();
        }
    }

    /// <summary>
    /// Runs the main logic of the application asynchronously.
    /// </summary>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task RunApplicationAsync()
    {
        while (true)
        {
            Console.WriteLine("Please enter the observation station id to get the average temperature (default is 94672 - Adelaide Airport). Enter 'exit' to quit:");
            var input = Console.ReadLine();

            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            string? apiUrl = $"{_configuration?["ApiSettings:WeatherApiUrl"]}{_configuration?["ApiSettings:AverageTemparatureUrlPath"]}";
            string? defaultObservationId = _configuration?["ApiSettings:DefaultObservationId"];
            string? observationStationId = string.IsNullOrEmpty(input) ? defaultObservationId : input;

            var temperatures = await GetAverageTemperature(apiUrl, observationStationId);

            if (temperatures == null)
            {
                Console.WriteLine("No temperature data available.");
            }
            else if (!string.IsNullOrEmpty(temperatures.LocationName))
            {
                Console.WriteLine($"Location: {temperatures.LocationName}, Average Temperature: {temperatures.AirTemp}°C");
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    /// <summary>
    /// Retrieves the average temperature from the weather API asynchronously.
    /// </summary>
    /// <param name="apiUrl">The base URL of the weather API.</param>
    /// <param name="observationStationId">The observation station ID for which to retrieve temperature data.</param>
    /// <returns>The average temperature data as a WeatherStationAverageTemparature object.</returns>
    public static async Task<WeatherStationAverageTemparature?> GetAverageTemperature(string? apiUrl, string? observationStationId)
    {
        try
        {
            var response = await client.GetAsync($"{apiUrl}?observationStationId={observationStationId}");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var temperatureRecords = JsonSerializer.Deserialize<WeatherStationAverageTemparature>(responseBody, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            return temperatureRecords;
        }
        catch (HttpRequestException ex) when (ex.InnerException is SocketException socketException &&
                                      socketException.SocketErrorCode == SocketError.ConnectionRefused)
        {
            // Handle the specific error when connection is actively refused
            Console.WriteLine($"Connection was actively refused. Check if the server is running on {apiUrl}.");
            return null;
        }
        catch (HttpRequestException e) when (e.InnerException is AuthenticationException)
        {
            Console.WriteLine($"SSL error occurred in deployed api: {e.InnerException.Message}");
            return null;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            Console.WriteLine($"The resource was not found for observationStationId: {observationStationId}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            return null;
        }
    }
}
