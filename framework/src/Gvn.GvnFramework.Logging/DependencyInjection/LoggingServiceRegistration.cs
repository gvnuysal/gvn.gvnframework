using Gvn.GvnFramework.Logging.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Gvn.GvnFramework.Logging.DependencyInjection;

public static class LoggingServiceRegistration
{
    public static IHostBuilder AddGvnLogging(
        this IHostBuilder builder,
        string applicationName)
    {
        return builder.UseSerilog((context, _, loggerConfig) =>
        {
            var cfg = SerilogConfiguration.CreateDefaultConfiguration(context.Configuration, applicationName);
            Log.Logger = cfg.CreateLogger();
            loggerConfig.ReadFrom.Configuration(context.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File("logs/log-.txt", rollingInterval: Serilog.RollingInterval.Day);
        });
    }

    public static IHostApplicationBuilder AddGvnLogging(
        this IHostApplicationBuilder builder,
        string applicationName)
    {
        var logger = SerilogConfiguration
            .CreateDefaultConfiguration(builder.Configuration, applicationName)
            .CreateLogger();

        builder.Services.AddSerilog(logger);

        return builder;
    }
}
