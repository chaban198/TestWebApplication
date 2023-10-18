using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface IProjectsService
{
    Task<ProjectDto?> GetProjectAsync(Guid id, CancellationToken cancellationToken);

    Task<Guid> CreateProjectAsync(CreateProjectRequest request, CancellationToken cancellationToken);

    Task UpdateProjectAsync(UpdateProjectRequest request, CancellationToken cancellationToken);

    Task DeleteProjectAsync(string id, CancellationToken cancellationToken);
}