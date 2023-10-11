using IdentityServer.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace IdentityServer.Controllers;

[ApiController]
[Route("[controller]")]
public class RegistrationController : Controller
{
    private readonly IIdentityUserService _userService;

    public RegistrationController(IIdentityUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var result = await _userService.Register(new IdentityUser(request.Username), request.Password);

        return result.Succeeded
            ? Accepted()
            : BadRequest(result.ToString());
    }
}