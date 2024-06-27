using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using WeatherAPI.Business.Services;
using WeatherAPI.Shared.Config;
using WeatherAPI.Shared.Interfaces;
using WeatherAPI.Shared.Models;

namespace WeatherAPI.Test.UnitTest
{
    [TestFixture]
    public class WeatherServiceTests
    {
        private WeatherService _weatherService;
        private Mock<IBOMAPIService> _BOMAPIServiceMock;
        private Mock<ILogger<WeatherService>> _loggerMock;
        private Mock<IOptions<WeatherAPIConfiguration>> _configMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILogger<WeatherService>>();
            _BOMAPIServiceMock = new Mock<IBOMAPIService>();
            var config = new WeatherAPIConfiguration
            {
                DefaultObservationStationId = 456,
                RelativePath = "test/<WMO>/data.json"
            };
            _configMock = new Mock<IOptions<WeatherAPIConfiguration>>();
            _configMock.Setup(c => c.Value).Returns(config);
            _weatherService = new WeatherService(_BOMAPIServiceMock.Object, _configMock.Object, _loggerMock.Object);
        }


        [Test]
        public async Task GetObservationData_WithValidObservationStationId_ReturnsData()
        {
            // Arrange
            int observationStationId = 123;
            List<WeatherStationRecord> weatherStationRecords = new(){
                new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=12.6,Dewpt=24.2},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=14.4,Dewpt=22.3},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=13.3,Dewpt=24.5},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=16.7,Dewpt=25.7},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=11.3,Dewpt=27.8},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=13.8,Dewpt=21.1},};
            DateTime now = DateTime.UtcNow;
            Random random = new Random();

            // Set Local_Date_Time_Full within the last 72 hours
            foreach (var record in weatherStationRecords)
            {
                DateTime randomDateTimeWithin72Hours = now.AddHours(-random.Next(0, 72));
                record.Local_Date_Time_Full = randomDateTimeWithin72Hours.ToString("yyyyMMddHHmmss");
            }
            var response = new Response
            {
                Observations = new Observations
                {
                    Data = weatherStationRecords
                }
            };

            _BOMAPIServiceMock.Setup(b => b.weatherStationRecordAsync(It.IsAny<string>())).ReturnsAsync(response);

            // Act
            var result = await _weatherService.GetObservationData(observationStationId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(6)); // Assert that we get 6 records as expected
        }

        [Test]
        public async Task GetAverageTemparature_WithValidObservationStationId_CorrectAverageTemparature()
        {
            // Arrange
            int observationStationId = 123;
            List<WeatherStationRecord> weatherStationRecords = new(){
                new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=12.6,Dewpt=24.2},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=14.4,Dewpt=22.3},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=13.3,Dewpt=24.5},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=16.7,Dewpt=25.7},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=11.3,Dewpt=27.8},
                    new WeatherStationRecord { Wmo = 123,Name="station1",Air_Temp=13.8,Dewpt=21.1},};
            DateTime now = DateTime.UtcNow;
            Random random = new Random();

            // Set Local_Date_Time_Full within the last 72 hours
            foreach (var record in weatherStationRecords)
            {
                DateTime randomDateTimeWithin72Hours = now.AddHours(-random.Next(0, 72));
                record.Local_Date_Time_Full = randomDateTimeWithin72Hours.ToString("yyyyMMddHHmmss");
            }

            var response = new Response
            {
                Observations = new Observations
                {
                    Data = weatherStationRecords
                }
            };

            _BOMAPIServiceMock.Setup(b => b.weatherStationRecordAsync(It.IsAny<string>())).ReturnsAsync(response);

            // Act
            var result = await _weatherService.GetAverageTemparature(observationStationId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.AirTemp, Is.EqualTo(13.683333333333332)); // Assert that we get the correct average temparaure
        }

    }
}