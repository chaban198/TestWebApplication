using AutoMapper;
using AutoMapper.QueryableExtensions;
using GlobalDomain.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using TaskListWebApplication.Data;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.DbModels;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public class SprintsService : ISprintsService
{
    private readonly TaskListApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public SprintsService(TaskListApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<SprintDto?> GetSprintAsync(Guid id, string? userLimitation, CancellationToken cancellationToken)
    {
        var ignoreUserLimitation = userLimitation is null;
        userLimitation ??= string.Empty;

        return await _dbContext.Sprints
            .Where(x => ignoreUserLimitation || x.Project.Users.Contains(userLimitation))
            .Where(x => x.Id == id)
            .ProjectTo<SprintDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SprintDto[]> GetSprintsByProjectIdAsync(Guid projectId, string? userLimitation, CancellationToken cancellationToken)
    {
        var ignoreUserLimitation = userLimitation is null;
        userLimitation ??= string.Empty;
        
        var projectExist = await _dbContext.Projects.AnyAsync(x => x.Id == projectId, cancellationToken);

        if (projectExist is false)
            throw new NotFoundException(nameof(ProjectDb), projectId);

        return await _dbContext.Sprints
            .Where(x => ignoreUserLimitation || x.Project.Users.Contains(userLimitation))
            .Where(x => x.ProjectId == projectId)
            .ProjectTo<SprintDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Guid> CreateSprintAsync(CreateSprintRequest request, CancellationToken cancellationToken)
    {
        var guid = Guid.NewGuid();

        var projectExist = await _dbContext.Projects.AnyAsync(x => x.Id == request.ProjectId, cancellationToken);

        if (projectExist is false)
            throw new NotFoundException(nameof(ProjectDb), request.ProjectId);

        _dbContext.Sprints.Add(new SprintDb
        {
            Id = guid,
            ProjectId = request.ProjectId,
            Name = request.SprintName,
            Description = request.SprintDescription,
            Comment = request.SprintComment,
            Start = DateTime.UtcNow
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        return guid;
    }

    public async Task UpdateSprintAsync(UpdateSprintRequest request, CancellationToken cancellationToken)
    {
        var sprint = await _dbContext.Sprints.FirstOrDefaultAsync(x => x.Id == request.SprintId, cancellationToken);

        if (sprint is null)
            throw new NotFoundException(nameof(SprintDb), request.SprintId);

        if (request.NewName is not null and not /*empty*/ "")
            sprint.Name = request.NewName;

        if (request.NewDescription is not null)
            sprint.Description = request.NewDescription;

        if (request.NewComment is not null)
            sprint.Comment = request.NewComment;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteSprintAsync(Guid id, CancellationToken cancellationToken)
    {
        var sprint = await _dbContext.Sprints.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (sprint is null)
            throw new NotFoundException(nameof(SprintDb), id);

        _dbContext.Remove(sprint);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}