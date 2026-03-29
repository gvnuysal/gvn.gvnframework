using Gvn.GvnFramework.Logging.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Gvn.GvnFramework.Logging.DependencyInjection;

/// <summary>
/// Extension methods for registering Serilog-based structured logging into the host.
/// </summary>
public static class LoggingServiceRegistration
{
    /// <summary>
    /// Configures Serilog on a classic <see cref="IHostBuilder"/> using <see cref="SerilogConfiguration.CreateDefaultConfiguration"/>.
    /// </summary>
    /// <param name="builder">The host builder to configure.</param>
    /// <param name="applicationName">The application name embedded in every log event.</param>
    /// <returns>The configured <see cref="IHostBuilder"/>.</returns>
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

    /// <summary>
    /// Configures Serilog on a modern <see cref="IHostApplicationBuilder"/> using <see cref="SerilogConfiguration.CreateDefaultConfiguration"/>.
    /// </summary>
    /// <param name="builder">The host application builder to configure.</param>
    /// <param name="applicationName">The application name embedded in every log event.</param>
    /// <returns>The configured <see cref="IHostApplicationBuilder"/>.</returns>
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
