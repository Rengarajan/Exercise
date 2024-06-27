using System.Collections.Generic;
using System.Threading.Tasks;
using WeatherAPI.Shared.Models;

namespace WeatherAPI.Shared.Interfaces
{
    /// <summary>
    /// Interface for retrieving weather data from a weather service.
    /// </summary>
    public interface IWeatherService
    {
        /// <summary>
        /// Retrieves observation data for a specific observation station asynchronously.
        /// </summary>
        /// <param name="observationStationId">The ID of the observation station to retrieve data for.</param>
        /// <returns>A task representing the asynchronous operation that returns a list of weather station records.</returns>
        Task<List<WeatherStationRecord>?> GetObservationData(int? observationStationId);

        /// <summary>
        /// Retrieves the average temperature data for a specific observation station asynchronously.
        /// </summary>
        /// <param name="observationStationId">The ID of the observation station to retrieve average temperature for.</param>
        /// <returns>A task representing the asynchronous operation that returns the average temperature.</returns>
        Task<WeatherStationAverageTemparature?> GetAverageTemparature(int? observationStationId);
    }
}
