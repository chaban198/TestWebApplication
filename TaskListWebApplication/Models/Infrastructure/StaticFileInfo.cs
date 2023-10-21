namespace TaskListWebApplication.Models.Infrastructure;

public readonly struct StaticFileInfo
{
    public string FileName { get; init; }
    public string? FileScope { get; init; }
    public Guid ScopeId { get; init; }
    public string FilePath => FileScope is not null
        ? $@"{FileScope}\{ScopeId}\{FileName}"
        : $"{FileName}";
}