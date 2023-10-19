using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface IProjectsService
{
    Task<Guid[]> GetProjectIdsAsync(string? userLimitation, CancellationToken cancellationToken = default);

    Task<ProjectDto?> GetProjectAsync(Guid id, string? userLimitation, CancellationToken cancellationToken = default);

    Task<Guid> CreateProjectAsync(CreateProjectRequest request, CancellationToken cancellationToken = default);

    Task UpdateProjectAsync(UpdateProjectRequest request, CancellationToken cancellationToken = default);

    Task DeleteProjectAsync(Guid id, CancellationToken cancellationToken = default);
}