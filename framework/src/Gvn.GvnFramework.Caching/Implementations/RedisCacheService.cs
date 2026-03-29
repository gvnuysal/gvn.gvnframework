using System.Text.Json;
using Gvn.GvnFramework.Caching.Abstractions;
using Gvn.GvnFramework.Caching.Configuration;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;

namespace Gvn.GvnFramework.Caching.Implementations;

/// <summary>
/// Redis-backed implementation of <see cref="ICacheService"/> using <see cref="IDistributedCache"/>.
/// Values are serialized to JSON before storage and deserialized on retrieval.
/// </summary>
public sealed class RedisCacheService(IDistributedCache cache, IOptions<CacheOptions> options) : ICacheService
{
    private readonly CacheOptions _options = options.Value;
    private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };

    private string PrefixKey(string key) => $"{_options.Prefix}{key}";

    /// <inheritdoc />
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        var value = await cache.GetStringAsync(PrefixKey(key), cancellationToken);
        return value is null ? default : JsonSerializer.Deserialize<T>(value, _jsonOptions);
    }

    /// <inheritdoc />
    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var serialized = JsonSerializer.Serialize(value, _jsonOptions);
        var entryOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiry ?? TimeSpan.FromMinutes(_options.DefaultExpiryMinutes)
        };
        await cache.SetStringAsync(PrefixKey(key), serialized, entryOptions, cancellationToken);
    }

    /// <inheritdoc />
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        => await cache.RemoveAsync(PrefixKey(key), cancellationToken);

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default)
        => await cache.GetAsync(PrefixKey(key), cancellationToken) is not null;

    /// <inheritdoc />
    public async Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CancellationToken cancellationToken = default)
    {
        var cached = await GetAsync<T>(key, cancellationToken);
        if (cached is not null) return cached;

        var value = await factory();
        await SetAsync(key, value, expiry, cancellationToken);
        return value;
    }
}
