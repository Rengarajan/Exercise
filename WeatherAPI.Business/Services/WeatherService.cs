using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherAPI.Shared.Config;
using WeatherAPI.Shared.Interfaces;
using WeatherAPI.Shared.Models;

namespace WeatherAPI.Business.Services
{
    /// <summary>
    /// Service class implementing IWeatherService for weather observation data operations.
    /// </summary>
    public class WeatherService : IWeatherService
    {
        private readonly IBOMAPIService _bOMAPIService;
        private readonly WeatherAPIConfiguration _config;
        private readonly ILogger<WeatherService> _logger;

        /// <summary>
        /// Constructor for WeatherService class.
        /// </summary>
        /// <param name="bOMAPIService">The BOM API service.</param>
        /// <param name="config">Options for configuring the Weather API.</param>
        /// <param name="logger">Logger instance for logging.</param>
        public WeatherService(IBOMAPIService bOMAPIService, IOptions<WeatherAPIConfiguration> config,
            ILogger<WeatherService> logger)
        {
            _bOMAPIService = bOMAPIService;
            _config = config.Value;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves weather observation data for a specified observation station ID.
        /// </summary>
        /// <param name="observationStationId">The ID of the observation station.</param>
        /// <returns>Returns a list of weather station records.</returns>
        public async Task<List<WeatherStationRecord>?> GetObservationData(int? observationStationId)
        {
            try
            {
                int defaultObservationStationId = _config.DefaultObservationStationId;
                observationStationId ??= defaultObservationStationId;

                var relativePath = $"{_config.RelativePath.Replace("<WMO>", observationStationId.ToString())}";

                var response = await _bOMAPIService.weatherStationRecordAsync(relativePath);
                return response.Observations.Data;
            }
            catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                _logger.LogInformation("The resource was not found for observationStationId: {ObservationStationId}", observationStationId);
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetObservationData: {ErrorMessage}", ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Retrieves the average temperature data for a specified observation station ID.
        /// </summary>
        /// <param name="observationStationId">The ID of the observation station.</param>
        /// <returns>Returns the average temperature data.</returns>
        public async Task<WeatherStationAverageTemparature?> GetAverageTemparature(int? observationStationId)
        {
            try
            {
                int defaultObservationStationId = _config.DefaultObservationStationId;
                observationStationId ??= defaultObservationStationId;
                var response = await GetObservationData(observationStationId);

                var averageTemperatureRecords = response?
                                     // Filtering records where Wmo id matches observationStationId
                                     .Where(record => record.Wmo == observationStationId)
                                     // Group filtered records by Wmo
                                     .GroupBy(record => record.Wmo) 
                                     .Select(group => new WeatherStationAverageTemparature
                                     {
                                         // Select the first Name in the group or default to observationStationId if null, with an additional fallback to empty string
                                         LocationName = group.Select(x => x.Name).First() ?? observationStationId?.ToString() ?? "",
                                         // Calculate the average Air_Temp in the group
                                         AirTemp = group.Average(record => record.Air_Temp) 
                                     })
                                     // Convert the result to a list
                                     .ToList()
                                     // Get the first item in the list or null if the list is empty
                                     .FirstOrDefault(); 


                return averageTemperatureRecords;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred in GetAverageTemparature: {ErrorMessage}", ex.Message);
                return null;
            }

        }
    }
}
