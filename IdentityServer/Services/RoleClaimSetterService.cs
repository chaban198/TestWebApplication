using IdentityServer4.Models;
using IdentityServer4.Services;

namespace Services;

public class RoleClaimSetterService : IProfileService
{
    public Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        context.IssuedClaims.AddRange(context.Subject.Claims);
        return Task.CompletedTask;
    }

    public Task IsActiveAsync(IsActiveContext context) => Task.CompletedTask;
}