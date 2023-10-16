using GlobalDomain.Helpers;
using GlobalDomain.Models.Configuration;
using IdentityServer.Models.Enums;
using IdentityServer.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;

[ApiController]
[Authorize(RoleSystem.Admin, AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Route("[controller]/[action]")]
public class AdminController : Controller
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpPost]
    public async Task<IActionResult> SetUserBan(string userName, bool makeUserBlocked = true)
    {
        if (User.GetUsername() == userName && makeUserBlocked)
            return Conflict("Администратор не может себя заблокировать");

        var result = makeUserBlocked
            ? await _adminService.BanUserAsync(userName)
            : await _adminService.UnbanUserAsync(userName);

        return result.Succeeded
            ? NoContent()
            : BadRequest(result.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> SetUserRole(string userName, AvailableRoles role = AvailableRoles.User)
    {
        if (User.GetUsername() == userName && role is not AvailableRoles.Admin)
            return Conflict("Администратор не может снять с себя роль");

        var result = await _adminService.SetUserRole(userName, role);

        return result.Succeeded
            ? NoContent()
            : BadRequest(result.ToString());
    }
}