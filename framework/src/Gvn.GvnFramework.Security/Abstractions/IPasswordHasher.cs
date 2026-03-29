namespace Gvn.GvnFramework.Security.Abstractions;

/// <summary>
/// Provides password hashing and verification.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the given plain-text password using a secure one-way algorithm.
    /// </summary>
    /// <param name="password">The plain-text password to hash.</param>
    /// <returns>The hashed representation of the password.</returns>
    string Hash(string password);

    /// <summary>
    /// Verifies that a plain-text password matches a previously computed hash.
    /// </summary>
    /// <param name="password">The plain-text password to verify.</param>
    /// <param name="hash">The hash to compare against.</param>
    /// <returns><c>true</c> if the password matches the hash; otherwise, <c>false</c>.</returns>
    bool Verify(string password, string hash);
}
