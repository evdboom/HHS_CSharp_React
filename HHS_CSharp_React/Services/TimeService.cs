
namespace HHS_CSharp_React.Services
{
    public class TimeService : ITimeService
    {
        public DateTime Now => DateTime.Now;

        public DateTime UtcNow => DateTime.UtcNow;

        public DateTime Today => DateTime.Today;
    }
}
