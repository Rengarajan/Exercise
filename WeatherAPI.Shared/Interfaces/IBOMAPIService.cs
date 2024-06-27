using Refit;
using WeatherAPI.Shared.Models;

namespace WeatherAPI.Shared.Interfaces
{
    /// <summary>
    /// Interface for accessing BOM API weather station records.
    /// </summary>
    public interface IBOMAPIService
    {
        /// <summary>
        /// Retrieves weather station records asynchronously from the BOM API.
        /// </summary>
        /// <param name="relativePath">The relative path to the API endpoint.</param>
        /// <returns>A task representing the asynchronous operation that returns a response.</returns>
        [Get("/{relativePath}")]
        Task<Response> weatherStationRecordAsync(string relativePath);
    }
}
