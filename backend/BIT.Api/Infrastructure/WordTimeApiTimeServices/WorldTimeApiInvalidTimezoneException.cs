using BIT.Api.Core;

namespace BIT.Api.Infrastructure.WordTimeApiTimeServices;

public class WorldTimeApiInvalidTimezoneException : ApiException
{
    public WorldTimeApiInvalidTimezoneException(string currentTimezone, string[] validTimezones)
    {
        CurrentTimezone = currentTimezone;
        ValidTimezones = validTimezones;
    }

    public override int StatusCode => 500;
    public override string Title => "Invalid timezone configuration.";

    public override string Details =>
        "Invalid timezone configuration. Set \"Timezone\" property in appsettings.json to fix this error.";
    public string[] ValidTimezones { get; }
    public string CurrentTimezone { get; }
}