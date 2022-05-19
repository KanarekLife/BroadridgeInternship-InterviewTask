using System.Net;
using BIT.Api.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration.Memory;
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

        response.EnsureSuccessStatusCode();
        response.Content.Headers.ContentType?.CharSet.Should().NotBeNull().And.Be("utf-8");
        response.Content.Headers.ContentType?.MediaType.Should().NotBeNull().And.Be("application/json");
    }

    [Fact]
    public async Task Should_ReturnException_ForInvalidTimezone()
    {
        var configuration = new MemoryConfigurationSource
        {
            InitialData = new[] { new KeyValuePair<string, string>("Timezone", "Europe/NewYork") }
        };
        var client = _factory
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration(conf => conf.Add(configuration));
            })
            .CreateClient();

        var response = await client.GetAsync("/time/");

        response.StatusCode.Should().Be(HttpStatusCode.BadGateway);
    }
}