using WeatherAPI.Shared.Models;

namespace WeatherAPI.GraphQLModels.Types
{
    /// <summary>
    /// GraphQL object type definition for representing weather station average temperature.
    /// </summary>
    public class WeatherStationAverageTemparatureType : ObjectType<WeatherStationAverageTemparature>
    {
        /// <summary>
        /// Configures the GraphQL schema for WeatherStationAverageTemparatureType.
        /// </summary>
        /// <param name="descriptor">The object type descriptor for WeatherStationAverageTemparature.</param>
        protected override void Configure(IObjectTypeDescriptor<WeatherStationAverageTemparature> descriptor)
        {
            descriptor.Name("WeatherStationAverageTemparature"); // Sets the GraphQL type name to "WeatherStationAverageTemparature"

            // Defines GraphQL fields for LocationName and AirTemp properties
            descriptor.Field(x => x.LocationName).Type<StringType>(); // Specifies the type of LocationName as StringType
            descriptor.Field(x => x.AirTemp).Type<FloatType>(); // Specifies the type of AirTemp as FloatType
        }
    }
}
