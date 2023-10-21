using AutoMapper;
using AutoMapper.QueryableExtensions;
using GlobalDomain.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using TaskListWebApplication.Data;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.DbModels;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public class ProjectsService : IProjectsService
{
    private readonly TaskListApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly IUsersService _usersService;
    private readonly IFilesStorage _filesStorage;

    public ProjectsService(TaskListApplicationDbContext dbContext, IMapper mapper, IUsersService usersService, IFilesStorage filesStorage)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _usersService = usersService;
        _filesStorage = filesStorage;
    }

    public async Task<Guid[]> GetProjectIdsAsync(string? username, CancellationToken cancellationToken = default)
    {
        var ignoreUserLimitation = username is null;
        username ??= string.Empty;

        return await _dbContext.Projects
            .Where(x => ignoreUserLimitation || x.Users.Contains(username))
            .Select(x => x.Id)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<ProjectDto?> GetProjectAsync(Guid id, string? userLimitation, CancellationToken cancellationToken = default)
    {
        var ignoreUserLimitation = userLimitation is null;
        userLimitation ??= string.Empty;

        return await _dbContext.Projects
            .Where(x => ignoreUserLimitation || x.Users.Contains(userLimitation))
            .ProjectTo<ProjectDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Guid> CreateProjectAsync(CreateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var guid = Guid.NewGuid();

        _dbContext.Projects.Add(new ProjectDb
        {
            Id = guid,
            Name = request.ProjectName,
            Description = request.ProjectDescription,
            Users = request.IncludeUsers.Distinct().ToList(),
        });

        await _dbContext.SaveChangesAsync(cancellationToken);
        return guid;
    }

    public async Task UpdateProjectAsync(UpdateProjectRequest request, CancellationToken cancellationToken = default)
    {
        var project = await _dbContext.Projects.FirstOrDefaultAsync(x => x.Id == request.ProjectId, cancellationToken);

        if (project is null)
            throw new NotFoundException(nameof(ProjectDb), request.ProjectId);

        if (request.NewName is not null and not /*empty*/ "")
            project.Name = request.NewName;

        if (request.NewDescription is not null)
            project.Description = request.NewDescription;

        if (request.IncludeUsers.Any())
        {
            var userValidation = await _usersService.CheckUsersAsync(request.IncludeUsers);
            if (userValidation.IsValid is false)
                throw new NotFoundException(userValidation.ToString());

            project.Users.AddRange(request.IncludeUsers.Distinct());
        }

        foreach (var exUser in request.ExcludeUsers.Distinct())
            if (project.Users.Contains(exUser))
                project.Users.Remove(exUser);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteProjectAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var project = await _dbContext.Projects
            .Include(projectDb => projectDb.Sprints)
            .ThenInclude(sprintDb => sprintDb.Tasks)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (project is null)
            throw new NotFoundException(nameof(ProjectDb), id);

        var sprintIds = project.Sprints
            .Select(x => x.Id)
            .ToArray();

        _dbContext.Remove(project);

        await _dbContext.SaveChangesAsync(cancellationToken);

        foreach (var sprintId in sprintIds)
            await _filesStorage.RemoveFilesScopeAsync(ISprintsService.StaticFilesScope, sprintId, cancellationToken);
    }
}