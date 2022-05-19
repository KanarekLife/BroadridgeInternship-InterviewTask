using BIT.Api.Services;

namespace BIT.ApiTests;

public class TestTimeService : ITimeService
{
    private readonly DateTime _currentTime;
    
    public TestTimeService(DateTime currentTime)
    {
        _currentTime = currentTime;
    }
    public Task<DateTime> GetCurrentTime()
    {
        return Task.FromResult(_currentTime);
    }
}