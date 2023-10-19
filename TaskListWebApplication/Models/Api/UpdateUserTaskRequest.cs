using TaskListWebApplication.Models.Enums;

namespace TaskListWebApplication.Models.Api;

public class UpdateUserTaskRequest
{

    public string? NewName { get; init; }

    public string? NewDescription { get; init; }

    public string? SetUser { get; init; }
    
    public UserTaskStatus? SetStatus { get; init; }
}