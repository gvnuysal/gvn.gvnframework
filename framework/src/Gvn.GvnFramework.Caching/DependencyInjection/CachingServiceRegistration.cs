using Gvn.GvnFramework.Caching.Abstractions;
using Gvn.GvnFramework.Caching.Configuration;
using Gvn.GvnFramework.Caching.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.Caching.DependencyInjection;

/// <summary>
/// Extension methods for registering caching services into the DI container.
/// </summary>
public static class CachingServiceRegistration
{
    /// <summary>
    /// Registers <see cref="ICacheService"/> using either Redis or in-memory caching
    /// based on the provided <see cref="CacheOptions"/>.
    /// </summary>
    /// <param name="services">The service collection to configure.</param>
    /// <param name="configure">An optional delegate to configure <see cref="CacheOptions"/>.</param>
    /// <returns>The configured <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddGvnCaching(
        this IServiceCollection services,
        Action<CacheOptions>? configure = null)
    {
        var options = new CacheOptions();
        configure?.Invoke(options);
        services.Configure<CacheOptions>(o =>
        {
            o.ConnectionString = options.ConnectionString;
            o.Prefix = options.Prefix;
            o.DefaultExpiryMinutes = options.DefaultExpiryMinutes;
            o.UseRedis = options.UseRedis;
        });

        if (options.UseRedis)
        {
            services.AddStackExchangeRedisCache(redisOptions =>
            {
                redisOptions.Configuration = options.ConnectionString;
            });
            services.AddSingleton<ICacheService, RedisCacheService>();
        }
        else
        {
            services.AddMemoryCache();
            services.AddSingleton<ICacheService, MemoryCacheService>();
        }

        return services;
    }
}
