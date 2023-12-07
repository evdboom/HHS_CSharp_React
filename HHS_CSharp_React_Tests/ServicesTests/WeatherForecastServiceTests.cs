namespace HHS_CSharp_React_Tests.ServicesTests
{
    public class WeatherForecastServiceTests : IDisposable
    {
        private readonly Mock<ITimeService> _timeService;
        private readonly MockTimeService _mockedTimeService;
        private DataContext _context;
        private WeatherForecastService _weatherForecastService;
        private readonly string _dbName;

        public WeatherForecastServiceTests()
        {
            _dbName = $"{Guid.NewGuid()}";
            _timeService = new Mock<ITimeService>();
            _context = ContextHelper.CreateContext(_dbName);
            _mockedTimeService = new();
            _weatherForecastService = new WeatherForecastService(_context, _mockedTimeService);
        }

        [Fact]
        public async Task GetsFromTodayPlusOne()
        {
            var today = new DateTime(2023, 12, 7);
            _mockedTimeService.NowResult = today;
            var list = await _weatherForecastService.Get();
            var item = list.First();
            Assert.Equal(today.AddDays(1), new DateTime(item.Date.Year, item.Date.Month, item.Date.Day));
        }

        [Fact]
        public async Task TemperatureFilterReturnsCorrectValues()
        {
            _context.AddRange(
            [
                new WeatherForecast
                {                 
                    Id = 1,
                    TemperatureC = 10,
                },
                new WeatherForecast
                {                 
                    Id = 2,
                    TemperatureC = 15,
                },
                new WeatherForecast
                {                    
                    Id = 3,
                    TemperatureC = 20,
                }
            ]);
            _context.SaveChanges();
            RefreshContext();

            _mockedTimeService.NowResult = new DateTime(2023, 7, 12);

            var result = await _weatherForecastService.GetWithMinimumTemperature(16);
            Assert.Collection(result, forecast =>
            {
                Assert.Equal(3, forecast.Id);
                Assert.Equal(20, forecast.TemperatureC);
            });
        }

        [Fact]
        public async Task TemperatureFilterReturnsCorrectValuesEdge()
        {
            _context.AddRange(
            [
                new WeatherForecast
                {
                    Id = 1,
                    TemperatureC = 10,
                },
                new WeatherForecast
                {
                    Id = 2,
                    TemperatureC = 15,
                },
                new WeatherForecast
                {
                    Id = 3,
                    TemperatureC = 20,
                }
            ]);

            _context.SaveChanges();
            RefreshContext();

            var result = await _weatherForecastService.GetWithMinimumTemperature(15);
            Assert.Collection(result, forecast =>
            {
                Assert.Equal(2, forecast.Id);
                Assert.Equal(15, forecast.TemperatureC);
            }, forecast =>
            {
                Assert.Equal(3, forecast.Id);
                Assert.Equal(20, forecast.TemperatureC);
            });
        }

        /// <summary>
        /// Refreshes the db context, can give a cleaner result because of changetracker
        /// </summary>
        private void RefreshContext()
        {
            _context = ContextHelper.CreateContext(_dbName);
            _weatherForecastService = new WeatherForecastService(_context, _timeService.Object);
        }

        private bool _disposed;
        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                _disposed = true;
                GC.SuppressFinalize(this);
            }
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

        }
    }
}
