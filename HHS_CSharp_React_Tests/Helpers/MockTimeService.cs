namespace HHS_CSharp_React_Tests.Helpers
{
    public class MockTimeService : ITimeService
    {
        public DateTime NowResult;
        public DateTime UtcNowResult;
        public DateTime TodayResult;
        public DateTime Now => NowResult;

        public DateTime UtcNow => UtcNowResult;

        public DateTime Today => TodayResult;
    }
}
