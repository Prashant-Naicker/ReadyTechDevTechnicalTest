using ReadyTechDevTechnicalTest.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IDateProvider, DateProvider>();
builder.Services.AddSingleton<ICoffeeProvider, CoffeeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
