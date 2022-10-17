using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Moq;
using NUnit.Framework;
using ReadyTechDevTechnicalTest.Common;
using ReadyTechDevTechnicalTest.Controllers;
using ReadyTechDevTechnicalTest.Domain;
using ReadyTechDevTechnicalTest.Integrations;
using ReadyTechDevTechnicalTest.Models;
using System;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class BrewCoffeeFixture
    {
        private Mock<IDateProvider> _dateProvider;
        private Mock<ICoffeeProvider> _coffeeProvider;
        private Mock<IWeatherService> _weatherService;

        private BrewCoffeeController _sut;

        private bool _isCoffeeAvailable;
        private DateTimeOffset _date;
        private decimal _temperature;

        [SetUp]
        public void Setup()
        {
            _dateProvider = new Mock<IDateProvider>(MockBehavior.Strict);
            _coffeeProvider = new Mock<ICoffeeProvider>(MockBehavior.Strict);
            _weatherService = new Mock<IWeatherService>(MockBehavior.Strict);

            _sut = new BrewCoffeeController(
                _dateProvider.Object,
                _coffeeProvider.Object,
                _weatherService.Object);

            _date = new DateTimeOffset(2022, 10, 16, 0, 0, 0, TimeSpan.Zero);
            _isCoffeeAvailable = false;
            _temperature = 300;

            _coffeeProvider
                .Setup(x => x.CoffeeAvailable())
                .Returns(() => _isCoffeeAvailable);

            _dateProvider
                .Setup(x => x.GetNow())
                .Returns(() => _date);

            _weatherService
                .Setup(x => x.GetCurrentWeatherAsync())
                .ReturnsAsync(() => new GetWeatherResponseModel
                {
                    Current = new WeatherModel
                    {
                        Temp = _temperature
                    }
                });
        }

        [Test]
        public async Task should_successfully_brew_coffee()
        {
            // Arrange & Act.
            var result = await _sut.BrewCoffee() as OkObjectResult;

            // Assert.
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            Assert.That(((BrewCoffeeResponseModel)result.Value).Message, Is.EqualTo("Your piping hot coffee is ready"));
            Assert.That(((BrewCoffeeResponseModel)result.Value).Prepared, Is.EqualTo(_date));            
        }

        [Test]
        public async Task should_return_service_unavailable_after_5_calls()
        {
            // Arrange
            _isCoffeeAvailable = true;
            
            // Act.
            var result = await _sut.BrewCoffee() as IStatusCodeActionResult;

            // Assert.
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(503));
        }

        [Test]
        public async Task should_return_I_am_a_teapot_april_first()
        {
            // Arrange.
            _date = new DateTimeOffset(2022, 04, 01, 0, 0, 0, TimeSpan.Zero);

            // Act.
            var result = await _sut.BrewCoffee() as IStatusCodeActionResult;

            // Assert.
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(418));
        }

        [Test]
        public async Task should_successfully_return_iced_coffee()
        {
            // Arrange.
            _temperature = 305;

            // Act.
            var result = await _sut.BrewCoffee() as OkObjectResult;

            // Assert.
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            Assert.That(((BrewCoffeeResponseModel)result.Value).Message, Is.EqualTo("Your refreshing iced coffee is ready"));
            Assert.That(((BrewCoffeeResponseModel)result.Value).Prepared, Is.EqualTo(_date));
        }
    }
}