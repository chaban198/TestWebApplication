using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface IUserTasksService
{
    Task<UserTaskDto?> GetUserTaskAsync(Guid id, CancellationToken cancellationToken);

    Task<Guid> CreateUserTaskAsync(CreateUserTaskRequest request, CancellationToken cancellationToken);

    Task UpdateUserTaskAsync(UpdateUserTaskRequest request, CancellationToken cancellationToken);

    Task DeleteUserTask(Guid id, CancellationToken cancellationToken);
}