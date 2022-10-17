using ReadyTechDevTechnicalTest.Models;

namespace ReadyTechDevTechnicalTest.Integrations
{
    public interface IWeatherService
    {
        Task<GetWeatherResponseModel> GetCurrentWeatherAsync();
    }
}
