using Newtonsoft.Json;

namespace ReadyTechDevTechnicalTest.Models
{
    public class BrewCoffeeResponseModel
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("prepared")]
        public DateTimeOffset Prepared { get; set; }
    }
}
