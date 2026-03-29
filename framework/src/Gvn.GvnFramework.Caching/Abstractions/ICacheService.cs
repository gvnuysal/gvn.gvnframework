namespace Gvn.GvnFramework.Caching.Abstractions;

/// <summary>
/// Provides a unified cache abstraction for storing, retrieving, and managing cached values.
/// Implementations may target Redis, in-memory, or any other backing store.
/// </summary>
public interface ICacheService
{
    /// <summary>
    /// Retrieves a cached value by key, deserializing it to <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The expected type of the cached value.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The deserialized value, or <c>default</c> if the key does not exist.</returns>
    Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stores a value in the cache under the specified key with an optional expiry.
    /// </summary>
    /// <typeparam name="T">The type of the value to cache.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="value">The value to store.</param>
    /// <param name="expiry">Optional absolute expiry duration. Uses <see cref="CacheOptions.DefaultExpiryMinutes"/> when <c>null</c>.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes the entry with the given key from the cache.
    /// </summary>
    /// <param name="key">The cache key to evict.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    Task RemoveAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Checks whether a cache entry exists for the given key.
    /// </summary>
    /// <param name="key">The cache key to check.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns><c>true</c> if the key exists in the cache; otherwise, <c>false</c>.</returns>
    Task<bool> ExistsAsync(string key, CancellationToken cancellationToken = default);

    /// <summary>
    /// Returns the cached value if present; otherwise invokes <paramref name="factory"/>,
    /// stores the result, and returns it.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    /// <param name="key">The cache key.</param>
    /// <param name="factory">An asynchronous factory that produces the value when there is a cache miss.</param>
    /// <param name="expiry">Optional absolute expiry duration.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The cached or newly created value.</returns>
    Task<T> GetOrSetAsync<T>(string key, Func<Task<T>> factory, TimeSpan? expiry = null, CancellationToken cancellationToken = default);
}
