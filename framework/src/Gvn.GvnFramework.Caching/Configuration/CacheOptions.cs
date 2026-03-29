namespace Gvn.GvnFramework.Caching.Configuration;

/// <summary>
/// Configuration options for the GvnFramework caching layer.
/// Bind from the <c>"Cache"</c> section of <c>appsettings.json</c>.
/// </summary>
public sealed class CacheOptions
{
    /// <summary>The configuration section key used to bind these options.</summary>
    public const string SectionName = "Cache";

    /// <summary>Gets or sets the Redis connection string. Defaults to <c>"localhost:6379"</c>.</summary>
    public string ConnectionString { get; set; } = "localhost:6379";

    /// <summary>Gets or sets the key prefix prepended to all cache keys. Defaults to <c>"gvn:"</c>.</summary>
    public string Prefix { get; set; } = "gvn:";

    /// <summary>Gets or sets the default cache entry expiry in minutes. Defaults to <c>60</c>.</summary>
    public int DefaultExpiryMinutes { get; set; } = 60;

    /// <summary>
    /// Gets or sets a value indicating whether to use Redis as the cache backend.
    /// When <c>false</c>, in-memory caching is used instead.
    /// </summary>
    public bool UseRedis { get; set; } = true;
}
