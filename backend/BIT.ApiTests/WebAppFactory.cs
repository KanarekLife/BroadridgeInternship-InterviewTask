using BIT.Api;
using BIT.Api.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

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
        base.ConfigureWebHost(builder);
    }
}