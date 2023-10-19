using TaskListWebApplication.Data;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public class UserTasksService : IUserTasksService
{
    private readonly TaskListApplicationDbContext _dbContext;

    public UserTasksService(TaskListApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Guid[]> GetAllUserTaskIdsAsync(string? username, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<UserTaskDto?> GetUserTaskAsync(Guid id, string? userLimitation, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task<Guid> CreateUserTaskAsync(CreateUserTaskRequest request, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task UpdateUserTaskAsync(UpdateUserTaskRequest request, CancellationToken cancellationToken) => throw new NotImplementedException();

    public Task DeleteUserTask(Guid id, CancellationToken cancellationToken) => throw new NotImplementedException();
}