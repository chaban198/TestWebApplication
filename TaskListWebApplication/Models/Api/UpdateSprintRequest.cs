using System.ComponentModel.DataAnnotations;

// ReSharper disable All

namespace TaskListWebApplication.Models.Api;

public class UpdateSprintRequest
{
    [Required]
    public Guid SprintId { get; init; }

    public string? NewName { get; init; }

    public string? NewDescription { get; init; }

    public string? NewComment { get; init; }
}