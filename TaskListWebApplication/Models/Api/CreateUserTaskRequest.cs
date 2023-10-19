using System.ComponentModel.DataAnnotations;

namespace TaskListWebApplication.Models.Api;

public class CreateUserTaskRequest
{
    [Required]
    public Guid SprintId { get; init; }

    [Required]
    public required string Name { get; init; }

    public string? Description { get; init; }

    public string? User { get; init; }
}