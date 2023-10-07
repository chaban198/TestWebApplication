using System.Security.Claims;

namespace GlobalDomain.Models.Configuration;

public static class RoleSystem
{
    public const string RoleClaim = ClaimTypes.Role;
    
    public const string Admin = "admin";
    public const string Manager = "manager";
    public const string User = "user";
}