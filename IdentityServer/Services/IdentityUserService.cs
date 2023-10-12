using System.Web;
using Microsoft.AspNetCore.Identity;

namespace Services;

public class IdentityUserService : IIdentityUserService
{
    private readonly IEMailService _mailService;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityUserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IEMailService mailService)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _mailService = mailService;
    }

    public async Task<IdentityResult> RegisterUserAsync(IdentityUser user, string password, string role)
    {
        if (await _roleManager.RoleExistsAsync(role) is false)
            throw new ArgumentException($"Role {role} is not exist", nameof(role));

        //create user
        var result = await _userManager.CreateAsync(user, password);
        if (result.Succeeded is false)
        {
            return result; //failed
        }

        //set role
        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (roleResult.Succeeded is false)
        {
            await _userManager.DeleteAsync(user);
            return roleResult; //failed role set, rollback user create
        }

        return result;
    }

    public async Task<IdentityResult> ResetPasswordAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return IdentityResult.Failed(new IdentityError[] { new() { Code = "404", Description = $"Not found user by name {userName}" } });

        if (user.Email is null)
            return IdentityResult.Failed(new IdentityError[] { new() { Code = "404", Description = "Not found user email" } });

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
        var message = $"Use this token to reset password: {HttpUtility.UrlEncode(resetToken)}";

        await _mailService.SendMail(user.Email, message, "Password reset");

        return IdentityResult.Success;
    }

    public async Task<IdentityResult> ConfirmResetPasswordAsync(string userName, string token, string newPassword)
    {
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
            return IdentityResult.Failed(new IdentityError[] { new() { Code = "404", Description = $"Not found user by name {userName}" } });

        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
        return result;
    }
}