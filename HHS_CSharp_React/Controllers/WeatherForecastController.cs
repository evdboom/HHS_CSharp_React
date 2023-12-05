using HHS_CSharp_React.Services;
using Microsoft.AspNetCore.Mvc;

namespace HHS_CSharp_React.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IWeatherForecastService _weatherForecastService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IWeatherForecastService weatherForecastService)
        {
            _logger = logger;
            _weatherForecastService = weatherForecastService;
        }

        [HttpGet("", Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            _logger.LogInformation("Got forecasting request, source: {0}", Request.Host.Host);
            return await _weatherForecastService
                .Get()
                .ConfigureAwait(continueOnCapturedContext: true);
        }

        [HttpGet("database", Name = "GetFromDataBase")]
        public async Task<IEnumerable<WeatherForecast>> GetFromDatabase()
        {
            _logger.LogInformation("Got forecasting request from database, source: {0}", Request.Host.Host);
            return await _weatherForecastService
                .GetFromDatabase()
                .ConfigureAwait(continueOnCapturedContext: true);
        }

        [HttpPost("database/search", Name = "GetWithTemperature")]        
        public async Task<IEnumerable<WeatherForecast>> SearchDatabase([FromBody] ForecastSearchRequest request)
        {
            _logger.LogInformation("Got forecasting request from database with minimum degrees, source: {0}", Request.Host.Host);
            return await _weatherForecastService
                .GetWithMinimumTemperature(request.MinimumDegrees)
                .ConfigureAwait(continueOnCapturedContext: true);
        }
    }
}
