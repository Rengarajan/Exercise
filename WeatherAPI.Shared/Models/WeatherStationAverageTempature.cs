namespace WeatherAPI.Shared.Models
{
    public class WeatherStationAverageTemparature
    {
        /// <summary>
        /// Gets or sets the name of the location for which average temperature is recorded.
        /// </summary>
        public string LocationName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the average air temperature at the location.
        /// </summary>
        public double? AirTemp { get; set; }
    }

}
