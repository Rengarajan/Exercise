using System.ComponentModel.DataAnnotations;

namespace WeatherAPI.Shared.Config
{
    public class WeatherAPIConfiguration
    {
        [Required]
        [Url]
        public required string Uri { get; init; }
        [Required]
        public required string RelativePath { get; init; }
        [Required]
        public required string DefaultObservationStationId { get; init; }

    }
   
}
