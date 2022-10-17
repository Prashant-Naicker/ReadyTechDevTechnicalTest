using ReadyTechDevTechnicalTest.Models;
using System.Text.Json;

namespace ReadyTechDevTechnicalTest.Integrations
{
    public class WeatherService : IWeatherService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<GetWeatherResponseModel> GetCurrentWeatherAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            const string api = "https://api.openweathermap.org/data/3.0/onecall?lat=-36.86&lon=174.74&appid=dd9128326c1e2e672462a897092ff10b";
            using var response = await httpClient.GetAsync(api);
            
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();
            var currentWeather = JsonSerializer.Deserialize<GetWeatherResponseModel>(json);

            return currentWeather;
        }
    }
}
