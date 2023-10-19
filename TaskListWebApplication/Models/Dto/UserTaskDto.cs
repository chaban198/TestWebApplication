using TaskListWebApplication.Models.Enums;

namespace TaskListWebApplication.Models.Dto;

public record UserTaskDto
{
    public Guid Id { get; init; }
    public Guid SprintId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public UserTaskStatus Status { get; init; }
    public string? User { get; init; }
}