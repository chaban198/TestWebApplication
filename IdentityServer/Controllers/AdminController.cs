using GlobalDomain.Models.Configuration;
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
    public async Task<IActionResult> BanUser(string userName, bool makeUserBlocked = true)
    {
        var result = makeUserBlocked
            ? await _adminService.BanUserAsync(userName)
            : await _adminService.UnbanUserAsync(userName);

        return result.Succeeded
            ? NoContent()
            : BadRequest(result.ToString());
    }
}