using System.ComponentModel.DataAnnotations;

// ReSharper disable All

namespace TaskListWebApplication.Models.Api;

public class UpdateProjectRequest
{
    [Required]
    public Guid ProjectId { get; init; }
    public string? NewName { get; init; }
    public string? NewDescription { get; init; }
    public string[] IncludeUsers { get; init; } = Array.Empty<string>();
    public string[] ExcludeUsers { get; init; } = Array.Empty<string>();
}