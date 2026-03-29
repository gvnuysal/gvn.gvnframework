using System.Security.Claims;

namespace Gvn.GvnFramework.Security.Abstractions;

/// <summary>
/// Provides JWT token generation and validation.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a signed JWT token from the provided claims.
    /// </summary>
    /// <param name="claims">The claims to embed in the token.</param>
    /// <returns>A signed JWT token string.</returns>
    string GenerateToken(IEnumerable<Claim> claims);

    /// <summary>
    /// Validates the given JWT token and returns the extracted claims principal.
    /// </summary>
    /// <param name="token">The JWT token string to validate.</param>
    /// <returns>
    /// A <see cref="ClaimsPrincipal"/> if the token is valid; otherwise, <c>null</c>.
    /// </returns>
    ClaimsPrincipal? ValidateToken(string token);
}
