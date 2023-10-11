using GlobalDomain.Models.Configuration;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Data.Seeders;

public class RoleSystemSeeder : ISeeder
{
    private readonly RoleManager<IdentityRole> _roleManager;

    public RoleSystemSeeder(RoleManager<IdentityRole> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Run()
    {
        await _roleManager.CreateAsync(new IdentityRole(RoleSystem.User));
        await _roleManager.CreateAsync(new IdentityRole(RoleSystem.Manager));
        await _roleManager.CreateAsync(new IdentityRole(RoleSystem.Admin));
    }
}