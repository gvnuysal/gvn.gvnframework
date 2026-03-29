using Gvn.GvnFramework.BackgroundJobs.Abstractions;
using Gvn.GvnFramework.BackgroundJobs.Authorization;
using Gvn.GvnFramework.BackgroundJobs.Configuration;
using Gvn.GvnFramework.BackgroundJobs.Implementations;
using Hangfire;
using Hangfire.Console;
using Hangfire.Dashboard;
using Hangfire.Heartbeat;
using Hangfire.InMemory;
using Hangfire.RecurringJobAdmin;
using Hangfire.Tags;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.BackgroundJobs.DependencyInjection;

/// <summary>
/// Extension methods for registering Hangfire background job services into the DI container.
/// </summary>
public static class BackgroundJobServiceRegistration
{
    /// <summary>
    /// Registers Hangfire storage, server, optional extensions (Console, Tags, Heartbeat, RecurringJobAdmin),
    /// and <see cref="IBackgroundJobService"/> using the provided configuration action.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configure">An optional delegate to configure <see cref="HangfireOptions"/>.</param>
    /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddGvnBackgroundJobs(
        this IServiceCollection services,
        Action<HangfireOptions>? configure = null)
    {
        var options = new HangfireOptions();
        configure?.Invoke(options);

        services.AddSingleton(options);

        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            config.UseSimpleAssemblyNameTypeSerializer();
            config.UseRecommendedSerializerSettings();

            if (options.UseInMemory)
                config.UseInMemoryStorage();

            if (options.UseConsole)
                config.UseConsole();

            if (options.UseTags)
                config.UseTags(new TagsOptions { TagsListStyle = TagsListStyle.Dropdown });

            if (options.UseHeartbeat)
                config.UseHeartbeatPage(checkInterval: TimeSpan.FromSeconds(10));

            if (options.UseRecurringJobAdmin)
                config.UseRecurringJobAdmin();
        });

        services.AddHangfireServer(serverOptions =>
        {
            serverOptions.WorkerCount = options.WorkerCount;
        });

        services.AddScoped<IBackgroundJobService, HangfireJobService>();

        return services;
    }

    /// <summary>
    /// Mounts the Hangfire dashboard at the specified path.
    /// Applies basic-auth protection when <see cref="HangfireOptions.DashboardUsername"/> and
    /// <see cref="HangfireOptions.DashboardPassword"/> are configured.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="path">The URL path to mount the dashboard on. Defaults to <c>"/hangfire"</c>.</param>
    /// <returns>The configured <see cref="IApplicationBuilder"/>.</returns>
    public static IApplicationBuilder UseGvnHangfireDashboard(
        this IApplicationBuilder app,
        string path = "/hangfire")
    {
        var options = app.ApplicationServices.GetService<HangfireOptions>() ?? new HangfireOptions();

        var dashboardOptions = new DashboardOptions
        {
            AppPath = null,
            DashboardTitle = "Gvn.GvnFramework — Hangfire Dashboard"
        };

        if (!string.IsNullOrWhiteSpace(options.DashboardUsername) &&
            !string.IsNullOrWhiteSpace(options.DashboardPassword))
        {
            dashboardOptions.Authorization =
            [
                new HangfireDashboardAuthorizationFilter(options.DashboardUsername, options.DashboardPassword)
            ];
        }

        app.UseHangfireDashboard(path, dashboardOptions);

        return app;
    }
}
