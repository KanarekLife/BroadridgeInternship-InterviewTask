namespace BIT.Api.Services;

public interface ITimeService
{
    Task<DateTime> GetCurrentTime();
}