namespace Gvn.GvnFramework.Security.Configuration;

/// <summary>
/// Configuration options for JWT Bearer authentication.
/// Bind from the <c>"Jwt"</c> section of <c>appsettings.json</c>.
/// </summary>
public sealed class JwtOptions
{
    /// <summary>The configuration section key used to bind these options.</summary>
    public const string SectionName = "Jwt";

    /// <summary>Gets or sets the HMAC-SHA256 signing secret. Must be at least 32 characters.</summary>
    public string Secret { get; set; } = default!;

    /// <summary>Gets or sets the token issuer, typically the application URL.</summary>
    public string Issuer { get; set; } = default!;

    /// <summary>Gets or sets the intended audience of the token.</summary>
    public string Audience { get; set; } = default!;

    /// <summary>Gets or sets the token lifetime in minutes. Defaults to <c>60</c>.</summary>
    public int ExpiryMinutes { get; set; } = 60;
}
