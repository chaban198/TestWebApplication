using GlobalDomain.Models.Configuration;
using IdentityServer.Config;
using Microsoft.AspNetCore.Identity;

namespace Services;

public class TestUsersSeeder : ISeeder
{
    private readonly IIdentityUserService _userService;

    public TestUsersSeeder(IIdentityUserService userService)
    {
        _userService = userService;
    }

    public async Task Run()
    {
        foreach (var testUser in IdentityConfig.TestUsers)
        {
            var identityUser = new IdentityUser(testUser.Username);
            var password = testUser.Password;
            var roleClaim = testUser.Claims.FirstOrDefault(x => x.Type is RoleSystem.RoleClaim);
            var role = roleClaim?.Value ?? RoleSystem.User;

            await _userService.Register(identityUser, password, role);
        }
    }
}