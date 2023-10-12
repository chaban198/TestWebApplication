using GlobalDomain.Models.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Services;

public interface IIdentityUserService
{
    public Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password, string role = RoleSystem.User);
    public Task<IdentityResult> ResetPasswordAsync(string userName);
    public Task<IdentityResult> ConfirmResetPasswordAsync(string userName, string token, string newPassword);
}