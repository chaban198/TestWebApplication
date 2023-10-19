using System.ComponentModel.DataAnnotations;
using TaskListWebApplication.Models.Enums;

// ReSharper disable All

namespace TaskListWebApplication.Models.Api;

public class UpdateUserTaskRequest
{
    [Required]
    public Guid TaskId { get; init; }
    public string? NewName { get; init; }
    public string? NewDescription { get; init; }
    public string? SetUser { get; init; }
    public UserTaskStatus? SetStatus { get; init; }
}