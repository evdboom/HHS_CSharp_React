using Microsoft.EntityFrameworkCore;

namespace HHS_CSharp_React.Infrastructure
{
    public class DataContext : DbContext
    {
        public DbSet<WeatherForecast> WeatherForecasts { get; set; }
    }
}
