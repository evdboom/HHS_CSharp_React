
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
        private readonly ITimeService _timeService;

        public WeatherForecastService(DataContext context, ITimeService timeService)
        {
            _context = context;
            _timeService = timeService;
        }

        public Task<IEnumerable<WeatherForecast>> Get()
        {
            InitTestData();
            return Task.FromResult(Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(_timeService.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }));
        }

        public async Task<IEnumerable<WeatherForecast>> GetFromDatabase()
        {
            InitTestData();
            // SELECT * FROM WeatherForecasts --(not actual *, EF will specify which columns to get)
            return await _context.WeatherForecasts
                .ToListAsync();
            
        }

        public async Task<IEnumerable<WeatherForecast>> GetWithMinimumTemperature(int minimumDegrees)
        {
            InitTestData();
            // SELECT * FROM WeatherForecasts WHERE TemperatureC >= @minimumDegrees --(not actual *, EF will specify which columns to get)
            return await _context.WeatherForecasts
                .Where(forecast => forecast.TemperatureC >= minimumDegrees)
                .ToListAsync();
        }

        // Code to make sure inmemory database has values, no place in a reallife scenario
        private void InitTestData()
        {
            
            if (!_context.WeatherForecasts.Any())
            {
                _context.WeatherForecasts.AddRange(Get().GetAwaiter().GetResult());
                _context.SaveChanges();
            }
        }
    }
}
