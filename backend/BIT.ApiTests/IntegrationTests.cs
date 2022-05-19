using BIT.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BIT.ApiTests;

public class Tests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;

    public Tests(WebAppFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task Should_ReturnCurrentTime_ForConfiguredTimezone()
    {
        var timeService = new TestTimeService(new DateTime(2020, 5, 19, 15, 06, 32));
        var client = _factory
            .WithWebHostBuilder(conf => 
                conf.ConfigureServices(services => 
                    services.AddSingleton(typeof(ITimeService), timeService)))
            .CreateClient();

        var response = await client.GetAsync("/time/");
        var content = await response.Content.ReadAsStringAsync();

        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.CharSet.Should().NotBeNull().And.Be("utf-8");
        response.Content.Headers.ContentType?.MediaType.Should().NotBeNull().And.Be("application/json");
        content.Should().Be("{\"time\":\"15:06:32\"}");
    }
}