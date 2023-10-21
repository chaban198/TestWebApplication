using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface IUserTasksService
{
    Task<Guid[]> GetAllUserTaskIdsAsync(string? username, CancellationToken cancellationToken);

    Task<UserTaskDto?> GetUserTaskAsync(Guid id, string? userLimitation, CancellationToken cancellationToken = default);

    Task<Guid> CreateUserTaskAsync(CreateUserTaskRequest request, CancellationToken cancellationToken = default);

    Task UpdateUserTaskAsync(UpdateUserTaskRequest request, CancellationToken cancellationToken = default);

    Task DeleteUserTaskAsync(Guid id, CancellationToken cancellationToken = default);
}