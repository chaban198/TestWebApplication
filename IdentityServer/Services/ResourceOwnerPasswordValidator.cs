using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace Services;

public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
{
    private readonly UserManager<IdentityUser> _userManager;

    public ResourceOwnerPasswordValidator(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
    {
        var user = await _userManager.FindByNameAsync(context.UserName);

        if (user is null || await _userManager.CheckPasswordAsync(user, context.Password) is not true)
        {
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "Invalid username or password");
            return;
        }

        context.Result = new GrantValidationResult(user.Id, OidcConstants.AuthenticationMethods.Password);
    }
}