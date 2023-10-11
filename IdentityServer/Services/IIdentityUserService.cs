using GlobalDomain.Models.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Services;

public interface IIdentityUserService
{
    public Task<IdentityResult> Register(IdentityUser user, string password, string role = RoleSystem.User);
}