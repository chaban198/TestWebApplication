using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface ISprintsService
{
    Task<SprintDto?> GetSprintAsync(Guid id, CancellationToken cancellationToken);

    Task<Guid> CreateSprintAsync(CreateSprintRequest request, CancellationToken cancellationToken = default);

    Task UpdateSprintAsync(UpdateSprintRequest request, CancellationToken cancellationToken);

    Task DeleteSprintAsync(string id, CancellationToken cancellationToken);
}