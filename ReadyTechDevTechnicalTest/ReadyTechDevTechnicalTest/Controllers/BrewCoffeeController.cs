using Microsoft.AspNetCore.Mvc;
using ReadyTechDevTechnicalTest.Common;
using ReadyTechDevTechnicalTest.Models;

namespace ReadyTechDevTechnicalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BrewCoffeeController : ControllerBase
    {
        private readonly IDateProvider _dateProvider;
        private readonly ICoffeeProvider _coffeeProvider;

        public BrewCoffeeController(
            IDateProvider dateProvider,
            ICoffeeProvider coffeeCounter)
        {
            _dateProvider = dateProvider;
            _coffeeProvider = coffeeCounter;
        }

        [HttpGet]
        public IActionResult BrewCoffee()
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

            return Ok(new BrewCoffeeResponseModel
            {
                Message = "Your piping hot coffee is ready",
                Prepared = now
            });
        }
    }
}
