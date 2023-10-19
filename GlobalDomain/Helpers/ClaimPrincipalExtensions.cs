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

    public static string? GetRole(this ClaimsPrincipal user)
    {
        var role = user.Claims.FirstOrDefault(x => x.Type is ClaimTypes.Role);

        return role?.Value;
    }
}