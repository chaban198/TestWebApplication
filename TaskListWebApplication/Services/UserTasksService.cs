using AutoMapper;
using AutoMapper.QueryableExtensions;
using GlobalDomain.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using TaskListWebApplication.Data;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.DbModels;
using TaskListWebApplication.Models.Dto;
using TaskListWebApplication.Models.Enums;

namespace TaskListWebApplication.Services;

public class UserTasksService : IUserTasksService
{
    private readonly TaskListApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserTasksService(TaskListApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<Guid[]> GetAllUserTaskIdsAsync(string? username, CancellationToken cancellationToken)
    {
        var ignoreUserLimitation = username is null;
        username ??= string.Empty;

        return await _dbContext.Tasks
            .Where(x => ignoreUserLimitation || x.Sprint.Project.Users.Contains(username))
            .Select(x => x.Id)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<UserTaskDto?> GetUserTaskAsync(Guid id, string? userLimitation, CancellationToken cancellationToken)
    {
        var ignoreUserLimitation = userLimitation is null;
        userLimitation ??= string.Empty;

        return await _dbContext.Tasks
            .Where(x => ignoreUserLimitation || x.Sprint.Project.Users.Contains(userLimitation))
            .ProjectTo<UserTaskDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateUserTaskAsync(CreateUserTaskRequest request, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();
        var sprintExist = await _dbContext.Projects.AnyAsync(x => x.Id == request.SprintId, cancellationToken);

        if (sprintExist is false)
            throw new NotFoundException(nameof(SprintDb), request.SprintId);

        _dbContext.Tasks.Add(new TaskDb
        {
            Id = guid,
            SprintId = request.SprintId,
            Name = request.Name,
            Description = request.Name,
            Status = UserTaskStatus.Open,
            User = request.User
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        return guid;
    }

    public async Task UpdateUserTaskAsync(UpdateUserTaskRequest request, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == request.TaskId, cancellationToken);

        if (task is null)
            throw new NotFoundException(nameof(TaskDb), request.TaskId);

        if (request.NewName is not null and not /*empty*/ "")
            task.Name = request.NewName;

        if (request.NewDescription is not null)
            task.Description = request.NewDescription;

        if (request.SetUser is not null)
            task.User = request.SetUser;

        if (request.SetStatus.HasValue)
            task.Status = request.SetStatus.Value;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteUserTask(Guid id, CancellationToken cancellationToken)
    {
        var task = await _dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (task is null)
            throw new NotFoundException(nameof(TaskDb), id);

        _dbContext.Remove(task);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}