namespace HHS_CSharp_React.Services
{
    public interface ITimeService
    {
        DateTime Now { get; }
        DateTime UtcNow { get; }
        DateTime Today { get; }        
    }
}
