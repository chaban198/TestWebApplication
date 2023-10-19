namespace TaskListWebApplication.Models.Api;

public class UpdateProjectRequest
{
    public string? NewName { get; init; }
    public string? NewDescription { get; init; }
    public string[] IncludeUsers { get; init; } = Array.Empty<string>();
    public string[] ExcludeUsers { get; init; } = Array.Empty<string>();
}