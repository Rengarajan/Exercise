using WeatherAPI.Shared.Models;

namespace WeatherAPI.GraphQLModels.Types
{
    /// <summary>
    /// GraphQL object type definition for representing weather station records.
    /// </summary>
    public class WeatherStationRecordType : ObjectType<WeatherStationRecord>
    {
        /// <summary>
        /// Configures the GraphQL schema for WeatherStationRecordType.
        /// </summary>
        /// <param name="descriptor">The object type descriptor for WeatherStationRecord.</param>
        protected override void Configure(IObjectTypeDescriptor<WeatherStationRecord> descriptor)
        {
            descriptor.Name("WeatherStationRecord"); // Sets the GraphQL type name to "WeatherStationRecord"

            // Define GraphQL fields for each property of WeatherStationRecord
            descriptor.Field(x => x.Sort_Order).Type<IntType>();
            descriptor.Field(x => x.Wmo).Type<IntType>();
            descriptor.Field(x => x.Name).Type<StringType>();
            descriptor.Field(x => x.History_Product).Type<StringType>();
            descriptor.Field(x => x.Local_Date_Time).Type<StringType>();
            descriptor.Field(x => x.Local_Date_Time_Full).Type<StringType>();
            descriptor.Field(x => x.Aifstime_Utc).Type<StringType>();
            descriptor.Field(x => x.Lat).Type<FloatType>();
            descriptor.Field(x => x.Lon).Type<FloatType>();
            descriptor.Field(x => x.Apparent_T).Type<FloatType>();
            descriptor.Field(x => x.Cloud).Type<StringType>();
            descriptor.Field(x => x.Cloud_Base_M).Type<IntType>();
            descriptor.Field(x => x.Cloud_Oktas).Type<IntType>();
            descriptor.Field(x => x.Cloud_Type_Id).Type<IntType>();
            descriptor.Field(x => x.Cloud_Type).Type<StringType>();
            descriptor.Field(x => x.Delta_T).Type<FloatType>();
            descriptor.Field(x => x.Gust_Kmh).Type<IntType>();
            descriptor.Field(x => x.Gust_Kt).Type<IntType>();
            descriptor.Field(x => x.Air_Temp).Type<FloatType>();
            descriptor.Field(x => x.Dewpt).Type<FloatType>();
            descriptor.Field(x => x.Press).Type<FloatType>();
            descriptor.Field(x => x.Press_Qnh).Type<FloatType>();
            descriptor.Field(x => x.Press_Msl).Type<FloatType>();
            descriptor.Field(x => x.Press_Tend).Type<StringType>();
            descriptor.Field(x => x.Rain_Trace).Type<StringType>();
            descriptor.Field(x => x.Rel_Hum).Type<IntType>();
            descriptor.Field(x => x.Sea_State).Type<StringType>();
            descriptor.Field(x => x.Swell_Dir_Worded).Type<StringType>();
            descriptor.Field(x => x.Swell_Height).Type<FloatType>();
            descriptor.Field(x => x.Swell_Period).Type<FloatType>();
            descriptor.Field(x => x.Vis_Km).Type<StringType>();
            descriptor.Field(x => x.Weather).Type<StringType>();
            descriptor.Field(x => x.Wind_Dir).Type<StringType>();
            descriptor.Field(x => x.Wind_Spd_Kmh).Type<IntType>();
            descriptor.Field(x => x.Wind_Spd_Kt).Type<IntType>();
        }
    }
}
