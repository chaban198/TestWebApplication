using System.Security.Claims;
using IdentityModel;

namespace GlobalDomain.Helpers;

public static class ClaimPrincipalExtensions
{
    public static string? GetUsername(this ClaimsPrincipal user)
    {
        var username = user.Claims.FirstOrDefault(x => x.Type is JwtClaimTypes.Name);

        return username?.Value;
    }
}