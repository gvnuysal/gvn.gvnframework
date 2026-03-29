using System.Security.Claims;

namespace Gvn.GvnFramework.Security.Abstractions;

public interface ITokenService
{
    string GenerateToken(IEnumerable<Claim> claims);
    ClaimsPrincipal? ValidateToken(string token);
}
