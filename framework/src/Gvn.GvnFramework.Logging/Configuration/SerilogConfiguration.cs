using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;

namespace Gvn.GvnFramework.Logging.Configuration;

/// <summary>
/// Provides a default Serilog logger configuration for GvnFramework applications.
/// </summary>
public static class SerilogConfiguration
{
    /// <summary>
    /// Creates a <see cref="LoggerConfiguration"/> with sensible defaults:
    /// console and rolling-file sinks, machine name/thread ID enrichers, and suppressed Microsoft/System noise.
    /// </summary>
    /// <param name="configuration">The application configuration used to override Serilog settings.</param>
    /// <param name="applicationName">The application name attached to every log event as the <c>Application</c> property.</param>
    /// <returns>A fully configured <see cref="LoggerConfiguration"/> instance ready to create a logger.</returns>
    public static LoggerConfiguration CreateDefaultConfiguration(
        IConfiguration configuration,
        string applicationName)
    {
        return new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProperty("Application", applicationName)
            .WriteTo.Console(
                outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}")
            .WriteTo.File(
                path: "logs/log-.txt",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {CorrelationId} {Message:lj}{NewLine}{Exception}")
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning);
    }
}
