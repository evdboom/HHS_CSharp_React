namespace HHS_CSharp_React.Services
{
    public interface IWeatherForecastService
    {
        Task<IEnumerable<WeatherForecast>> Get();
        Task<IEnumerable<WeatherForecast>> GetFromDatabase();
        Task<IEnumerable<WeatherForecast>> GetWithMinimumTemperature(int minumumDegrees);
    }
}
