using Microsoft.AspNetCore.Identity;

namespace Services;

public class IdentityUserService : IIdentityUserService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public IdentityUserService(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<IdentityResult> Register(IdentityUser user, string password, string role)
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
}