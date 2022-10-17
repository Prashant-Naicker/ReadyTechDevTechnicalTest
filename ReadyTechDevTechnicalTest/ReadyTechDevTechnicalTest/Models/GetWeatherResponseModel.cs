using Newtonsoft.Json;

namespace ReadyTechDevTechnicalTest.Models
{
    public class GetWeatherResponseModel
    {
        [JsonProperty("current")]
        public WeatherModel Current { get; set; }
    }

    public class WeatherModel
    {
        [JsonProperty("temp")]
        public decimal Temp { get; set; }
    }
}
