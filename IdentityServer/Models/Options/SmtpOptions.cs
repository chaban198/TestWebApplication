// ReSharper disable All

namespace IdentityServer.Models.Options;

public record SmtpOptions
{
    public string? Host { get; init; }
    public int Port { get; init; }
    public string? EMail { get; init; }
    public string? Password { get; init; }
}