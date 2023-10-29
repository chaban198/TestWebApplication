using IdentityServer.Models.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace IdentityServer.Controllers;

[ApiController]
[AllowAnonymous]
[Route("[controller]/[action]")]
public class UserAccountController : Controller
{
    private readonly IIdentityUserService _userService;

    private const string TokenConnectUrl = "/connect/token";
    private const string UrlencodedUrlContentType = "application/x-www-form-urlencoded";

    public UserAccountController(IIdentityUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    [Route(TokenConnectUrl)]
    [Consumes(UrlencodedUrlContentType)]
    public IActionResult Login([FromForm] LoginByPasswordRequest request)
    {
        //SWAGGER declaration only!
        throw new NotSupportedException("Запрос не был отправлен в Identity server");
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterUserRequest request)
    {
        var user = new IdentityUser(request.Username)
        {
            Email = request.EMail,
            EmailConfirmed = true
        };

        var result = await _userService.RegisterUserAsync(user, request.Password);

        return result.Succeeded
            ? Accepted()
            : BadRequest(result.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(string userName)
    {
        var result = await _userService.ResetPasswordAsync(userName);

        return result.Succeeded
            ? Accepted()
            : BadRequest(result.ToString());
    }

    [HttpPost]
    public async Task<IActionResult> ConfirmResetPassword(string userName, string token, string newPassword)
    {
        var result = await _userService.ConfirmResetPasswordAsync(userName, token, newPassword);

        return result.Succeeded
            ? Accepted()
            : BadRequest(result.ToString());
    }
}