using Gvn.GvnFramework.Caching.Abstractions;
using Gvn.GvnFramework.Caching.Configuration;
using Gvn.GvnFramework.Caching.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Gvn.GvnFramework.Caching.DependencyInjection;

public static class CachingServiceRegistration
{
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
