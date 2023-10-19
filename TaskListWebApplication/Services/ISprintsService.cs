using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface ISprintsService
{
    Task<SprintDto?> GetSprintAsync(Guid id, string? userLimitation, CancellationToken cancellationToken = default);

    Task<SprintDto[]> GetSprintsByProjectIdAsync(Guid projectId, string? userLimitation, CancellationToken cancellationToken = default);

    Task<Guid> CreateSprintAsync(CreateSprintRequest request, CancellationToken cancellationToken = default);

    Task UpdateSprintAsync(UpdateSprintRequest request, CancellationToken cancellationToken = default);

    Task DeleteSprintAsync(Guid id, CancellationToken cancellationToken = default);
}