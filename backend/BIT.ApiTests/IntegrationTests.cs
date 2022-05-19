using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration.Memory;
using Xunit;
using Xunit.Abstractions;

namespace BIT.ApiTests;

public class Tests : IClassFixture<WebAppFactory>
{
    private readonly WebAppFactory _factory;
    private readonly ITestOutputHelper _output;

    public Tests(WebAppFactory factory, ITestOutputHelper output)
    {
        _factory = factory;
        _output = output;
    }

    [Fact]
    public async Task Should_ReturnCurrentTime_ForConfiguredTimezone()
    {
        var configuration = new MemoryConfigurationSource
        {
            InitialData = new[] { new KeyValuePair<string, string>("Timezone", "Europe/Warsaw") }
        };
        var client = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration(conf => conf.Add(configuration));
            })
            .CreateClient();

        var response = await client.GetAsync("/time/");
        var content = await response.Content.ReadAsStringAsync();
        _output.WriteLine(content);

        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.CharSet.Should().NotBeNull().And.Be("utf-8");
        response.Content.Headers.ContentType?.MediaType.Should().NotBeNull().And.Be("application/json");
        content.Should().NotBeEmpty();
    }

    [Theory]
    [InlineData("")]
    [InlineData("Europe/NewYork")]
    public async Task Should_ReturnException_ForInvalidTimezone(string timezone)
    {
        var configuration = new MemoryConfigurationSource
        {
            InitialData = new[] { new KeyValuePair<string, string>("Timezone", timezone) }
        };
        var client = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration(conf => conf.Add(configuration));
            })
            .CreateClient();

        var response = await client.GetAsync("/time/");
        var content = await response.Content.ReadAsStringAsync();
        _output.WriteLine(content);

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
        content.Should().NotBeEmpty();
    }
}