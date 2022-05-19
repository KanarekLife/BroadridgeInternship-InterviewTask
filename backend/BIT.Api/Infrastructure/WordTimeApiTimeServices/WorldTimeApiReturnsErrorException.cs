using BIT.Api.Core;

namespace BIT.Api.Infrastructure.WordTimeApiTimeServices;

public class WorldTimeApiReturnsErrorException : ApiException
{
    public override int StatusCode => 502;
    public override string Title => "World Time Api has returned invalid response.";

    public override string Details =>
        "World Time Api has returned invalid response. Check configured timezone in application's configuration and verify if api is up.";
}