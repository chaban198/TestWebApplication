using TaskListWebApplication.Data;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public class SprintsService : ISprintsService
{
    private readonly TaskListApplicationDbContext _dbContext;

    public SprintsService(TaskListApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<SprintDto?> GetSprintAsync(Guid id, string? userLimitation, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<SprintDto[]> GetSprintsByProjectIdAsync(Guid projectId, string? userLimitation, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Guid> CreateSprintAsync(CreateSprintRequest request, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task UpdateSprintAsync(UpdateSprintRequest request, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task DeleteSprintAsync(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
}