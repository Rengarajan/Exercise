using Microsoft.Extensions.Options;
using WeatherAPI.Shared.Config;
namespace WeatherAPI.Config
{
    /// <summary>
    /// Validates the configuration options for the Weather API settings.
    /// Implements the <see cref="IValidateOptions{WeatherAPIConfiguration}" /> interface.
    /// </summary>
    [OptionsValidator]
    public partial class WeatherAPIConfigurationValidator : IValidateOptions<WeatherAPIConfiguration>
    {
    }
}
