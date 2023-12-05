using HHS_CSharp_React.Infrastructure;
using HHS_CSharp_React.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddScoped<IWeatherForecastService, WeatherForecastService>()
    .AddSingleton<ITimeService, TimeService>();
builder.Services
    .AddDbContext<DataContext>(config =>
    {
        config.UseInMemoryDatabase("WeatherDb");
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
