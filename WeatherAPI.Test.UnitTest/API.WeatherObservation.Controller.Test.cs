using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherAPI.Controllers;
using WeatherAPI.Shared.Interfaces;
using WeatherAPI.Shared.Models;

namespace WeatherAPI.Test.UnitTest
{
    [TestFixture]
    public class WeatherObservationControllerTests
    {
        private SouthAustraliaWeatherObservationController _controller;
        private Mock<IWeatherService> _weatherServiceMock;
        private Mock<ILogger<SouthAustraliaWeatherObservationController>> _loggerMock;

        [SetUp]
        public void Setup()
        {
            _weatherServiceMock = new Mock<IWeatherService>();
            _loggerMock = new Mock<ILogger<SouthAustraliaWeatherObservationController>>();

            _controller = new SouthAustraliaWeatherObservationController(_loggerMock.Object, _weatherServiceMock.Object);
        }

        [Test]
        public async Task GetWeatherObservationData_WithValidObservationStationId_ReturnsOk()
        {
            // Arrange
            int observationStationId = 123;
            string field = "";
            List<WeatherStationRecord> weatherStationRecords = new(){ 
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 10.5 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 11.2 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 9.8 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 13.5 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 15.2 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 7.8 },};
            var expectedData = weatherStationRecords;

            _weatherServiceMock.Setup(service => service.GetObservationData(observationStationId))
                               .ReturnsAsync(expectedData);

            // Act
            var result = await _controller.GetWeatherObservationData(observationStationId, field) as ObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status200OK));
                Assert.That(result.Value, Is.Not.Null);
            });
        }

        [Test]
        public async Task GetWeatherObservationData_WithValidObservationStationId_ReturnsOnlySpecifiedFields()
        {
            // Arrange
            int observationStationId = 123;
            string field = "name,air_temP";
            List<WeatherStationRecord> weatherStationRecords = new(){
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 10.5 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 11.2 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 9.8 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 13.5 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 15.2 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 7.8 },};
            var expectedData = weatherStationRecords;

            _weatherServiceMock.Setup(service => service.GetObservationData(observationStationId))
                               .ReturnsAsync(expectedData);

            // Act
            var result = await _controller.GetWeatherObservationData(observationStationId, field) as ObjectResult;
            var resultList = result?.Value as List<Dictionary<string, string>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(resultList, Is.Not.Null);
            });
            Assert.That(resultList.Count, Is.EqualTo(6));

            // Verify fields returned for each record
            foreach (var record in resultList)
            {
                Assert.That(record.Count, Is.EqualTo(2)); // Ensure each record has exactly two fields
                // Only two fields: Name and Temperatureas per 'field' parameter
            }

        }

        [Test]
        public async Task GetWeatherObservationData_WithValidObservationStationId_ReturnsOnlySingleFields()
        {
            // Arrange
            int observationStationId = 123;
            string field = "air_temp";
            List<WeatherStationRecord> weatherStationRecords = new(){
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 10.5 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 11.2 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 9.8 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 13.5 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 15.2 },
                new WeatherStationRecord { Wmo = 123, Name = "Station A", Air_Temp = 7.8 },};
            var expectedData = weatherStationRecords;

            _weatherServiceMock.Setup(service => service.GetObservationData(observationStationId))
                               .ReturnsAsync(expectedData);

            // Act
            var result = await _controller.GetWeatherObservationData(observationStationId, field) as ObjectResult;
            var resultList = result?.Value as List<Dictionary<string, string>>;

            // Assert
            Assert.IsNotNull(result);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(resultList, Is.Not.Null);
            });
            Assert.That(resultList.Count, Is.EqualTo(6));

            // Verify fields returned for each record
            foreach (var record in resultList)
            {
                Assert.That(record.Count, Is.EqualTo(1)); // Ensure each record has exactly two fields
                // Only two fields: Name and Temperatureas per 'field' parameter
            }

        }

        [Test]
        public async Task GetWeatherObservationData_WithInvalidObservationStationId_ReturnsNotFound()
        {
            // Arrange
            int observationStationId = 999; // Invalid ID
            string field = "testfield";

            _weatherServiceMock.Setup(service => service.GetObservationData(observationStationId))
                               .ReturnsAsync(new List<WeatherStationRecord>());

            // Act
            var result = await _controller.GetWeatherObservationData(observationStationId, field) as ObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(StatusCodes.Status404NotFound));
                Assert.That(result.Value, Is.EqualTo($"No record found for the Observation Station ID: {observationStationId}"));
            });
        }

        [Test]
        public async Task GetWeatherObservationAverageTempature_ReturnsOk_WhenRecordExists()
        {
            // Arrange
            int observationStationId = 1;
            var expectedAverageTemperature = new WeatherStationAverageTemparature { AirTemp = 15.4, LocationName = "Station A" };
            _weatherServiceMock.Setup(service => service.GetAverageTemparature(observationStationId))
                               .ReturnsAsync(expectedAverageTemperature);

            // Act
            var result = await _controller.GetWeatherObservationAverageTempature(observationStationId) as ObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(200));
                Assert.That(result.Value, Is.EqualTo(expectedAverageTemperature));
            });
        }

        [Test]
        public async Task GetWeatherObservationAverageTempature_ReturnsNotFound_WhenNoRecordFound()
        {
            // Arrange
            int observationStationId = 1;
            var expectedAverageTemperature = new WeatherStationAverageTemparature { AirTemp = 15.4, LocationName = "Station A" };
            _weatherServiceMock.Setup(service => service.GetAverageTemparature(observationStationId))
                               .ReturnsAsync((WeatherStationAverageTemparature?)null);

            // Act
            var result = await _controller.GetWeatherObservationAverageTempature(observationStationId) as ObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(result.StatusCode, Is.EqualTo(404));
                Assert.That(result.Value, Is.EqualTo($"No record found for the Observation Station ID: {observationStationId}"));
            });
        }


    }
}