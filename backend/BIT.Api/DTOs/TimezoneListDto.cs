namespace BIT.Api.DTOs;

public class TimezoneListDto
{
    public TimezoneListDto(string[] availableTimezones)
    {
        AvailableTimezones = availableTimezones;
    }

    public string[] AvailableTimezones { get; }
}