using GlobalDomain.Models.Configuration;
using IdentityServer.Helpers;
using IdentityServer.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services;

public class AdminService : IAdminService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AdminService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> BanUserAsync(string userName) => await SetLockoutEnabledByNameAsync(userName, true);

    public async Task<IdentityResult> UnbanUserAsync(string userName) => await SetLockoutEnabledByNameAsync(userName, false);

    public async Task<IdentityResult> SetUserRole(string userName, AvailableRoles role = AvailableRoles.User)
    {
        var pureRole = role switch
        {
            AvailableRoles.User => RoleSystem.User,
            AvailableRoles.Manager => RoleSystem.Manager,
            AvailableRoles.Admin => RoleSystem.Admin,
            _ => throw new ArgumentOutOfRangeException(nameof(role), role, null)
        };

        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return CustomIdentityErrors.UserNotFound(userName);

        var currentRole = await _userManager.GetRolesAsync(user);

        //already actual case check
        if (currentRole.Contains(pureRole))
            return IdentityResult.Success;

        //clear old role
        if (currentRole.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRole);

        //set actual role
        return await _userManager.AddToRoleAsync(user, pureRole);
    }

    private async Task<IdentityResult> SetLockoutEnabledByNameAsync(string userName, bool value)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return CustomIdentityErrors.UserNotFound(userName);

        return await _userManager.SetLockoutEnabledAsync(user, value);
    }
}