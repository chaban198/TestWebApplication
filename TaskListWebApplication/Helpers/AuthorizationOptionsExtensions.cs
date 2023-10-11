using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using static GlobalDomain.Models.Configuration.RoleSystem;

namespace TaskListWebApplication.Helpers;

public static class AuthorizationOptionsExtensions
{
    public static void AddRoleSystemPolicies(this AuthorizationOptions options)
    {
        const string roleClaim = ClaimTypes.Role;

        options.AddPolicy(Admin, policy => policy.RequireClaim(roleClaim, Admin));
        options.AddPolicy(Manager, policy => policy.RequireClaim(roleClaim, Admin, Manager));
        options.AddPolicy(User, policy => policy.RequireClaim(roleClaim, Admin, Manager, User));
    }
}