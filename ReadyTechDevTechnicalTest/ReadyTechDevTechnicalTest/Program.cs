using ReadyTechDevTechnicalTest.Common;
using ReadyTechDevTechnicalTest.Domain;
using ReadyTechDevTechnicalTest.Integrations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IDateProvider, DateProvider>();
builder.Services.AddSingleton<ICoffeeProvider, CoffeeProvider>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
