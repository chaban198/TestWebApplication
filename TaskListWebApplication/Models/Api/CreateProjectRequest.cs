using System.ComponentModel.DataAnnotations;

// ReSharper disable All

namespace TaskListWebApplication.Models.Api;

public class CreateProjectRequest
{
    [Required]
    public required string ProjectName { get; init; }
    public string? ProjectDescription { get; init; }
    public string[] IncludeUsers { get; init; } = Array.Empty<string>();
}