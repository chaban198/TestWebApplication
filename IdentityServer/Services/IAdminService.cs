using IdentityServer.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Services;

public interface IAdminService
{
    Task<IdentityResult> BanUserAsync(string userName);

    Task<IdentityResult> UnbanUserAsync(string userName);

    Task<IdentityResult> SetUserRole(string userName, AvailableRoles role = AvailableRoles.User);
}