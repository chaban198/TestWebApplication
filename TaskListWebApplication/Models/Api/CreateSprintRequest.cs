using System.ComponentModel.DataAnnotations;

namespace TaskListWebApplication.Models.Api;

public class CreateSprintRequest
{
    [Required]
    public Guid ProjectId { get; init; }

    [Required]
    public required string SprintName { get; init; }

    public string? SprintDescription { get; init; }

    public string? SprintComment { get; init; }
}