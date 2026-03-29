using Gvn.GvnFramework.Caching.Abstractions;
using Gvn.GvnFramework.Caching.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace Gvn.GvnFramework.Caching.Implementations;

/// <summary>
/// In-memory implementation of <see cref="ICacheService"/> backed by <see cref="IMemoryCache"/>.
/// Suitable for single-node deployments where distributed caching is not required.
/// </summary>
public sealed class MemoryCacheService(IMemoryCache cache, IOptions<CacheOptions> options) : ICacheService
{
    private readonly CacheOptions _options = options.Value;

    /// <inheritdoc />
    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        cache.TryGetValue(key, out T? value);
        return Task.FromResult(value);
    }

    /// <inheritdoc />
    public Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var entryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(_options.DefaultExpiryMinutes)
        };
        cache.Set(key, value, entryOptions);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        cache.Remove(key);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        => Task.FromResult(cache.TryGetValue(key, out _));

    /// <inheritdoc />
    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        if (cache.TryGetValue(key, out T? cached) && cached is not null)
            return cached;

        var value = await factory();
        await SetAsync(key, value, expiry, cancellationToken);
        return value;
    }
}
