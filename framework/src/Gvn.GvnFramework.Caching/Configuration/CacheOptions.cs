namespace Gvn.GvnFramework.Caching.Configuration;

public sealed class CacheOptions
{
    public const string SectionName = "Cache";

    public string ConnectionString { get; set; } = "localhost:6379";
    public string Prefix { get; set; } = "gvn:";
    public int DefaultExpiryMinutes { get; set; } = 60;
    public bool UseRedis { get; set; } = true;
}
