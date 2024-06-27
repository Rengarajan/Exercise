using Microsoft.AspNetCore.Mvc;
using WeatherAPI.Shared.Interfaces;
using WeatherAPI.Shared.Models;

namespace WeatherAPI.Controllers
{
    /// <summary>
    /// API controller for handling weather observation data specific to South Australia.
    /// Provides endpoints to retrieve detailed observation data and average temperatures.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class SouthAustraliaWeatherObservationController : ControllerBase
    {
        private readonly ILogger<SouthAustraliaWeatherObservationController> _logger;
        private readonly IWeatherService _weatherService;

        /// <summary>
        /// Constructor for the SouthAustraliaWeatherObservationController class.
        /// </summary>
        /// <param name="logger">Logger instance for logging.</param>
        /// <param name="weatherService">Service for weather data operations.</param>
        public SouthAustraliaWeatherObservationController(ILogger<SouthAustraliaWeatherObservationController> logger, IWeatherService weatherService)
        {
            _logger = logger;
            _weatherService = weatherService;
        }

        /// <summary>
        /// Retrieves weather observation data for a specified observation station ID.
        /// </summary>
        /// <param name="observationStationId">The ID of the observation station.</param>
        /// <param name="field">Optional. Filters the returned fields by a comma-separated list of field names.</param>
        /// <returns>Returns a list of dictionaries containing observation data fields.</returns>
        [HttpGet("GetWeatherObservationData")]
        public async Task<IActionResult> GetWeatherObservationData([FromQuery] int? observationStationId, [FromQuery] string? field)
        {
            try
            {
                var record = await _weatherService.GetObservationData(observationStationId);
                if (record == null || record.Count == 0)
                {
                    _logger.LogInformation("No record found for Observation Station ID: {observationStationId}", observationStationId);
                    return NotFound($"No record found for the Observation Station ID: {observationStationId}");
                }

                var fieldList = await FilterObservationDataAsync(record, field);
                if (fieldList.Count == 0)
                {
                    _logger.LogInformation("No matching fields found for Observation Station ID: {observationStationId}", observationStationId);
                    return NotFound($"No record found for the Observation Station ID: {observationStationId}");
                }

                return Ok(fieldList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An error occurred in GetWeatherObservationData for Observation Station ID: {observationStationId}. Error: {ErrorMessage}",
                    observationStationId, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Retrieves the average temperature data for a specified observation station ID.
        /// </summary>
        /// <param name="observationStationId">The ID of the observation station.</param>
        /// <returns>Returns the average temperature data.</returns>
        [HttpGet("GetWeatherObservationAverageTempature")]
        public async Task<IActionResult> GetWeatherObservationAverageTempature([FromQuery] int? observationStationId)
        {
            try
            {
                var record = await _weatherService.GetAverageTemparature(observationStationId);
                if (record == null)
                {
                    _logger.LogInformation("No record found for Observation Station ID: {observationStationId}", observationStationId);
                    return NotFound($"No record found for the Observation Station ID: {observationStationId}");
                }

                return Ok(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,
                    "An error occurred in GetWeatherObservationAverageTempature for Observation Station ID: {ObservationStationId}. Error: {ErrorMessage}",
                    observationStationId, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Filters weather observation data based on the specified fields asynchronously.
        /// </summary>
        /// <param name="weatherStationRecords">List of weather station records.</param>
        /// <param name="field">Comma-separated list of field names to filter.</param>
        /// <returns>Returns a list of dictionaries containing filtered observation data.</returns>
        private async Task<List<Dictionary<string, string?>>> FilterObservationDataAsync(List<WeatherStationRecord> weatherStationRecords, string? field)
        {
            var fieldList = new List<Dictionary<string, string?>>();
            var properties = weatherStationRecords
                .SelectMany(item => item.GetType().GetProperties())
                // Filter properties based on field parameter, if provided
                .Where(property => string.IsNullOrEmpty(field) || field.ToLower().Split(',').Contains(property.Name.ToLower()))
                // Ensure each property is distinct by its name (case-insensitive comparison)
                .DistinctBy(property => property.Name.ToLower());


            foreach (var item in weatherStationRecords)
            {
                var fieldValues = properties.ToDictionary(
                    property => property.Name,
                    property => property.GetValue(item)?.ToString()
                );

                if (fieldValues.Any())
                {
                    fieldList.Add(fieldValues);
                }
            }

            return await Task.FromResult(fieldList);
        }

    }

}
