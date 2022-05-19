namespace BIT.Api.DTOs;

public class CurrentTimeDto
{
    public CurrentTimeDto(string time)
    {
        Time = time;
    }

    public string Time { get; }
}