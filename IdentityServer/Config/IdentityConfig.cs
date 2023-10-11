using System.Security.Claims;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using static GlobalDomain.Models.Configuration.RoleSystem;

namespace IdentityServer.Config;

public static class IdentityConfig
{
    private const string TaskListApiScope = "task-list-scope";
    private const string TaskListApiResource = "task-list-api";

    public static IEnumerable<Client> Clients => new Client[]
    {
        new()
        {
            ClientId = "task-list-test-client",
            ClientSecrets = { new Secret("secret".Sha256()) },
            AlwaysIncludeUserClaimsInIdToken = true,
            AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
            AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, TaskListApiScope }
        }
    };

    public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };

    public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
    {
        new(TaskListApiScope, "Task-List Web API", new[] { RoleClaim })
    };

    public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
    {
        new(TaskListApiResource) { Scopes = { TaskListApiScope } }
    };

    public static List<TestUser> TestUsers => new()
    {
        new TestUser
        {
            SubjectId = "a3ea0f25-b464-506f-bc1e-c925176244b9",
            Username = "Debug User",
            Password = "123456qwerty",
            Claims = new List<Claim> { new(RoleClaim, User) }
        },
        new TestUser
        {
            SubjectId = "a3ea0f25-b464-506f-bc1e-c925176244b9",
            Username = "Debug User2",
            Password = "123456qwerty",
            Claims = new List<Claim> { new(RoleClaim, Admin) }
        }
    };
}