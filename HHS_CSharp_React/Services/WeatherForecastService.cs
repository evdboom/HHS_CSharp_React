
using HHS_CSharp_React.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace HHS_CSharp_React.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        private readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];
        private readonly DataContext _context;

        public WeatherForecastService(DataContext context)
        {
            _context = context;

            // Code to make sure inmemory database has values)
            if (!_context.WeatherForecasts.Any())
            {
                _context.WeatherForecasts.AddRange(Get().GetAwaiter().GetResult());
                _context.SaveChanges();
            }
        }

        public Task<IEnumerable<WeatherForecast>> Get()
        {
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }));
        }

        public async Task<IEnumerable<WeatherForecast>> GetFromDatabase()
        {
            return await _context.WeatherForecasts
                .ToListAsync();
        }

        public async Task<IEnumerable<WeatherForecast>> GetWithMinimumTemperature(int minumumDegrees)
        {
            return await _context.WeatherForecasts
                .Where(forecast => forecast.TemperatureC >= minumumDegrees)
                .ToListAsync();
        }
    }
}
