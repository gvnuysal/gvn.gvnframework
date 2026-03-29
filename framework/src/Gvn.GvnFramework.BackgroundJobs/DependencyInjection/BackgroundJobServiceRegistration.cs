using Gvn.GvnFramework.BackgroundJobs.Abstractions;
using Gvn.GvnFramework.BackgroundJobs.Configuration;
using Gvn.GvnFramework.BackgroundJobs.Implementations;
using Hangfire;
using Hangfire.InMemory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.BackgroundJobs.DependencyInjection;

public static class BackgroundJobServiceRegistration
{
    public static IServiceCollection AddGvnBackgroundJobs(
        this IServiceCollection services,
        Action<HangfireOptions>? configure = null)
    {
        var options = new HangfireOptions();
        configure?.Invoke(options);

        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_180);
            config.UseSimpleAssemblyNameTypeSerializer();
            config.UseRecommendedSerializerSettings();

            if (options.UseInMemory)
                config.UseInMemoryStorage();
        });

        services.AddHangfireServer(serverOptions =>
        {
            serverOptions.WorkerCount = options.WorkerCount;
        });

        services.AddScoped<IBackgroundJobService, HangfireJobService>();

        return services;
    }

    public static IApplicationBuilder UseGvnHangfireDashboard(
        this IApplicationBuilder app,
        string path = "/hangfire")
    {
        app.UseHangfireDashboard(path, new DashboardOptions
        {
            AppPath = null
        });
        return app;
    }
}
