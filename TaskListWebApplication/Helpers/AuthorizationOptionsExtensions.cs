using Microsoft.AspNetCore.Authorization;
using static GlobalDomain.Models.Configuration.RoleSystem;

namespace TaskListWebApplication.Helpers;

public static class AuthorizationOptionsExtensions
{
    public static void AddRoleSystemPolicies(this AuthorizationOptions options)
    {
        options.AddPolicy(Admin, policy => policy.RequireClaim(RoleClaim, Admin));
        options.AddPolicy(Manager, policy => policy.RequireClaim(RoleClaim, Admin, Manager));
        options.AddPolicy(User, policy => policy.RequireClaim(RoleClaim, Admin, Manager, User));
    }
}