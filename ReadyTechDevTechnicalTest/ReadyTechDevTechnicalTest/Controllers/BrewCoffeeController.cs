using Microsoft.AspNetCore.Mvc;
using ReadyTechDevTechnicalTest.Common;
using ReadyTechDevTechnicalTest.Domain;
using ReadyTechDevTechnicalTest.Integrations;
using ReadyTechDevTechnicalTest.Models;

namespace ReadyTechDevTechnicalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrewCoffeeController : ControllerBase
    {
        private readonly IDateProvider _dateProvider;
        private readonly ICoffeeProvider _coffeeProvider;
        private readonly IWeatherService _weatherService;

        public BrewCoffeeController(
            IDateProvider dateProvider,
            ICoffeeProvider coffeeCounter,
            IWeatherService weatherService)
        {
            _dateProvider = dateProvider;
            _coffeeProvider = coffeeCounter;
            _weatherService = weatherService;
        }

        [HttpGet]
        public async Task<IActionResult> BrewCoffee()
        {
            if (_coffeeProvider.CoffeeAvailable())
            {
                return StatusCode(503);
            }

            var now = _dateProvider.GetNow();

            if (now.Month == 04 && now.Day == 01)
            {
                return StatusCode(418);
            }

            var currentWeather = await _weatherService.GetCurrentWeatherAsync();
            var tempInC = currentWeather.Current.Temp - (decimal)273.15;

            return Ok(new BrewCoffeeResponseModel
            {
                Message = tempInC > 30 ? "Your refreshing iced coffee is ready" : "Your piping hot coffee is ready",
                Prepared = now
            });
        }
    }
}
