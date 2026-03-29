using Gvn.GvnFramework.Security.Abstractions;

namespace Gvn.GvnFramework.Security.Implementations;

/// <summary>
/// BCrypt implementation of <see cref="IPasswordHasher"/>.
/// Uses enhanced BCrypt hashing (SHA-384 pre-hash) for secure, salted password storage.
/// </summary>
public sealed class BcryptPasswordHasher : IPasswordHasher
{
    /// <inheritdoc />
    public string Hash(string password)
        => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    /// <inheritdoc />
    public bool Verify(string password, string hash)
        => BCrypt.Net.BCrypt.EnhancedVerify(password, hash);
}
