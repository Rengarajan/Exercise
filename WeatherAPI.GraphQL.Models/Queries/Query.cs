using WeatherAPI.Shared.Interfaces;
using WeatherAPI.Shared.Models;

namespace WeatherAPI.GraphQLModels.Queries
{
    /// <summary>
    /// GraphQL query resolver class for weather station data.
    /// </summary>
    public class Query
    {
        private readonly IWeatherService _weatherService;

        /// <summary>
        /// Constructor for Query class.
        /// </summary>
        /// <param name="weatherService">The weather service dependency.</param>
        public Query(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        /// <summary>
        /// Retrieves weather station records for a specified observation station ID.
        /// </summary>
        /// <param name="observationStationId">The ID of the observation station.</param>
        /// <returns>A task that represents the asynchronous operation. Returns a list of weather station records.</returns>
        public async Task<List<WeatherStationRecord>?> GetWeatherStationRecords(int? observationStationId)
        {
            return await _weatherService.GetObservationData(observationStationId);
        }

        /// <summary>
        /// Retrieves the average temperature for a specified observation station ID.
        /// </summary>
        /// <param name="observationStationId">The ID of the observation station.</param>
        /// <returns>A task that represents the asynchronous operation. Returns the average temperature data.</returns>
        public async Task<WeatherStationAverageTemparature?> GetWeatherStationAverageTemparature(int? observationStationId)
        {
            return await _weatherService.GetAverageTemparature(observationStationId);
        }
    }
}
