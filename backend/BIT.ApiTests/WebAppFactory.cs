using BIT.Api;
using BIT.Api.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace BIT.ApiTests;

public class WebAppFactory : WebApplicationFactory<Startup>
{
    // Required due to .NET 6 [https://github.com/dotnet/AspNetCore.Docs/issues/7063#issuecomment-414661566]
    protected override IWebHostBuilder CreateWebHostBuilder()
    {
        return WebHost.CreateDefaultBuilder()
            .UseStartup<Startup>();
    }
    
    // Required due to .NET 6 [https://github.com/dotnet/AspNetCore.Docs/issues/7063#issuecomment-414661566]
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseContentRoot(".");
        builder.ConfigureServices(services =>
        {
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(ITimeService));
            if (descriptor is not null)
            {
                services.Remove(descriptor);
            }
            // Each test should inject ITimeServiceImplementation
        });
        base.ConfigureWebHost(builder);
    }
}