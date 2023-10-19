using System.Diagnostics.CodeAnalysis;

namespace TaskListWebApplication.Models.Dto;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record SprintDto
{
    public Guid Id { get; init; }
    public Guid ProjectId { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public DateTime Start { get; init; }
    public DateTime? End { get; init; }
    public string? Comment { get; init; }
    public string[] Files { get; init; } = Array.Empty<string>();
}