using TaskListWebApplication.Data;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public class ProjectsService : IProjectsService
{
    private readonly TaskListApplicationDbContext _dbContext;

    public ProjectsService(TaskListApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<ProjectDto?> GetProjectAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Guid> CreateProjectAsync(CreateProjectRequest request, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task UpdateProjectAsync(UpdateProjectRequest request, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task DeleteProjectAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
}