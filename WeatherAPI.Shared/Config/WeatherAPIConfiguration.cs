using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Shared.Config
{
    /// <summary>
    /// Configuration class for Weather API settings.
    /// </summary>
    public class WeatherAPIConfiguration
    {
        [Required(ErrorMessage = "The Uri field is required.")]
        [Url(ErrorMessage = "The Uri field must be a valid URL.")]
        public string Uri { get; init; }

        [Required(ErrorMessage = "The RelativePath field is required.")]
        public string RelativePath { get; init; }

        [Required(ErrorMessage = "The DefaultObservationStationId field is required.")]
        public int DefaultObservationStationId { get; init; }
        [Required(ErrorMessage = "The FilterPreviousHours field is required.")]
        public int FilterPreviousHours { get; init; }
    }
}
