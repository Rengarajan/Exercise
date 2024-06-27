using Newtonsoft.Json;
namespace WeatherAPI.Shared.Models
{
    
    using System.Collections.Generic;

    public class Notice
    {
        [JsonProperty("copyright")]
        public string? Copyright { get; set; }

        [JsonProperty("copyright_url")]
        public string? CopyrightUrl { get; set; }

        [JsonProperty("disclaimer_url")]
        public string? DisclaimerUrl { get; set; }

        [JsonProperty("feedback_url")]
        public string? FeedbackUrl { get; set; }
    }

    public class Header
    {
        [JsonProperty("refresh_message")]
        public string? RefreshMessage { get; set; }

        [JsonProperty("ID")]
        public string? ID { get; set; }

        [JsonProperty("main_ID")]
        public string? MainID { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("state_time_zone")]
        public string? StateTimeZone { get; set; }

        [JsonProperty("time_zone")]
        public string? TimeZone { get; set; }

        [JsonProperty("product_name")]
        public string? ProductName { get; set; }

        [JsonProperty("state")]
        public string? State { get; set; }
    }

  

    public class Observations
    {
        [JsonProperty("notice")]
        public List<Notice>? Notice { get; set; }

        [JsonProperty("header")]
        public List<Header>? Header { get; set; }

        [JsonProperty("data")]
        public List<WeatherStationRecord>? Data { get; set; }
    }


    public class Response {
        [JsonProperty("observations")]
        public Observations Observations { get; set; }

    }


}
