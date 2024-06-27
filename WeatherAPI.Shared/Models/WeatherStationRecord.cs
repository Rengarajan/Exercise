using Newtonsoft.Json;

namespace WeatherAPI.Shared.Models
{
    public class WeatherStationRecord
    {
        [JsonProperty("sort_order")]
        public int? Sort_Order { get; set; }

        [JsonProperty("wmo")]
        public int? Wmo { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("history_product")]
        public string? History_Product { get; set; }

        [JsonProperty("local_date_time")]
        public string? Local_Date_Time { get; set; }

        [JsonProperty("local_date_time_full")]
        public string Local_Date_Time_Full { get; set; }

        [JsonProperty("aifstime_utc")]
        public string? Aifstime_Utc { get; set; }

        [JsonProperty("lat")]
        public double? Lat { get; set; }

        [JsonProperty("lon")]
        public double? Lon { get; set; }

        [JsonProperty("apparent_t")]
        public double? Apparent_T { get; set; }

        [JsonProperty("cloud")]
        public string? Cloud { get; set; }

        [JsonProperty("cloud_base_m")]
        public int? Cloud_Base_M { get; set; }

        [JsonProperty("cloud_oktas")]
        public int? Cloud_Oktas { get; set; }

        [JsonProperty("cloud_type_id")]
        public int? Cloud_Type_Id { get; set; }

        [JsonProperty("cloud_type")]
        public string? Cloud_Type { get; set; }

        [JsonProperty("delta_t")]
        public double? Delta_T { get; set; }

        [JsonProperty("gust_kmh")]
        public int? Gust_Kmh { get; set; }

        [JsonProperty("gust_kt")]
        public int? Gust_Kt { get; set; }

        [JsonProperty("air_temp")]
        public double? Air_Temp { get; set; }

        [JsonProperty("dewpt")]
        public double? Dewpt { get; set; }

        [JsonProperty("press")]
        public double? Press { get; set; }

        [JsonProperty("press_qnh")]
        public double? Press_Qnh { get; set; }

        [JsonProperty("press_msl")]
        public double? Press_Msl { get; set; }

        [JsonProperty("press_tend")]
        public string? Press_Tend { get; set; }

        [JsonProperty("rain_trace")]
        public string? Rain_Trace { get; set; }

        [JsonProperty("rel_hum")]
        public int? Rel_Hum { get; set; }

        [JsonProperty("sea_state")]
        public string? Sea_State { get; set; }

        [JsonProperty("swell_dir_worded")]
        public string? Swell_Dir_Worded { get; set; }

        [JsonProperty("swell_height")]
        public double? Swell_Height { get; set; }

        [JsonProperty("swell_period")]
        public double? Swell_Period { get; set; }

        [JsonProperty("vis_km")]
        public string? Vis_Km { get; set; }

        [JsonProperty("weather")]
        public string? Weather { get; set; }

        [JsonProperty("wind_dir")]
        public string? Wind_Dir { get; set; }

        [JsonProperty("wind_spd_kmh")]
        public int? Wind_Spd_Kmh { get; set; }

        [JsonProperty("wind_spd_kt")]
        public int? Wind_Spd_Kt { get; set; }
    }
}
