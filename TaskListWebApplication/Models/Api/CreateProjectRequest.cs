using System.ComponentModel.DataAnnotations;

namespace TaskListWebApplication.Models.Api;

public class CreateProjectRequest
{
    [Required]
    public required string ProjectName { get; init; }
    public string? ProjectDescription { get; init; }
    public bool IncludeMe { get; init; }
    public string[] IncludeUsers { get; init; } = Array.Empty<string>();
}