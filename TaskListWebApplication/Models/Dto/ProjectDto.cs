using System.Diagnostics.CodeAnalysis;

namespace TaskListWebApplication.Models.Dto;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public record ProjectDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }
    public List<string> Users { get; init; } = new();
}