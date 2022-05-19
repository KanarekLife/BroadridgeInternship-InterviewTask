﻿using BIT.Api.Filters;
using BIT.Api.Infrastructure.WordTimeApiTimeServices;
using BIT.Api.Services;

namespace BIT.Api;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<ITimeService, WorldTimeApiTimeService>();
        services.AddControllers(conf =>
        {
            conf.Filters.Add<WorldTimeApiInvalidTimezoneFilter>();
            conf.Filters.Add<ApiExceptionFilter>();
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}