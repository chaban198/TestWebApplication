using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

// ReSharper disable All

namespace IdentityServer.Models.Api;

public record LoginByPasswordRequest
{
    [Required]
    [FromForm(Name = "client_id")] [DefaultValue("task-list-test-client")]
    public required string ClientId { get; init; }

    [Required]
    [FromForm(Name = "client_secret")] [DefaultValue("secret")]
    public required string ClientSecret { get; init; }

    [Required]
    [FromForm(Name = "grant_type")] [DefaultValue("password")]
    public required string GrantType { get; init; }

    [Required]
    [FromForm(Name = "username")] [DefaultValue("DebugAdmin")]
    public required string Username { get; init; }

    [Required]
    [FromForm(Name = "password")] [DefaultValue("String123!")]
    public required string Password { get; init; }
}