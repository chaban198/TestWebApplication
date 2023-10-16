using IdentityServer.Helpers;
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

    private async Task<IdentityResult> SetLockoutEnabledByNameAsync(string userName, bool value)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return CustomIdentityErrors.UserNotFound(userName);

        return await _userManager.SetLockoutEnabledAsync(user, value);
    }
}