// ReSharper disable All

using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Models.Api;

public record RegisterUserRequest
{
    [Required]
    public required string Username { get; init; }

    [Required]
    public required string Password { get; init; }

    [Required]
    [EmailAddress]
    public required string EMail { get; init; }
}