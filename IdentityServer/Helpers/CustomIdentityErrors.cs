using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Helpers;

public static class CustomIdentityErrors
{
    public static IdentityResult UserNotFound(string userName) => IdentityResult.Failed(new IdentityError[]
    {
        new()
        {
            Code = "UserNotFound",
            Description = $"Not found user by name {userName}"
        }
    });

    public static IdentityResult UserEmailNotFound => IdentityResult.Failed(new IdentityError[]
    {
        new()
        {
            Code = "EmailNotFound",
            Description = "Not found user email"
        }
    });
}