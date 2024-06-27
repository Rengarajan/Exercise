using WeatherAPI.GraphQLModels.Queries;
using HotChocolate.Types;

namespace WeatherAPI.GraphQLModels.Types
{
    /// <summary>
    /// GraphQL object type definition for queries related to weather stations.
    /// </summary>
    public class QueryType : ObjectType<Query>
    {
        /// <summary>
        /// Configures the GraphQL schema for the QueryType.
        /// </summary>
        /// <param name="descriptor">The object type descriptor for Query.</param>
        protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
        {
            // Configure the field for retrieving weather station records
            descriptor.Field(f => f.GetWeatherStationRecords(default))
                      .Type<ListType<WeatherStationRecordType>>()
                      .Name("weatherStationRecords")
                      .Argument("observationStationId", a => a.Type<IntType>());

            // Configure the field for retrieving weather station average temperature
            descriptor.Field(f => f.GetWeatherStationAverageTemparature(default))
                     .Type<WeatherStationAverageTemparatureType>()
                     .Name("weatherStationAverageTemarature")
                     .Argument("observationStationId", a => a.Type<IntType>());
        }
    }
}
