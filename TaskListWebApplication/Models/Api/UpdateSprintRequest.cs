namespace TaskListWebApplication.Models.Api;

public class UpdateSprintRequest
{
    public string? NewName { get; init; }

    public string? NewDescription { get; init; }

    public string? NewComment { get; init; }
}