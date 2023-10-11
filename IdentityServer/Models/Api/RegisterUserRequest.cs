// ReSharper disable All

namespace IdentityServer.Models.Api;

public record RegisterUserRequest
{
    public required string Username { get; init; }
    public required string Password { get; init; }
}