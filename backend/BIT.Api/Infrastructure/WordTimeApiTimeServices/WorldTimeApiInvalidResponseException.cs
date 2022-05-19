using BIT.Api.Core;

namespace BIT.Api.Infrastructure.WordTimeApiTimeServices;

public class WorldTimeApiInvalidResponseException : ApiException
{
    public override int StatusCode => 502;
    public override string Title => "Invalid response received from World Time Api.";
    public override string Details => "Invalid response received from World Time Api. Please contact app's support.";
}