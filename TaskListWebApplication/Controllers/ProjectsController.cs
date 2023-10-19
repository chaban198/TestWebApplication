using GlobalDomain.Helpers;
using GlobalDomain.Models.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;
using TaskListWebApplication.Services;

namespace TaskListWebApplication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }

    [HttpGet]
    [Authorize(RoleSystem.Manager)]
    public async Task<ActionResult<Guid[]>> GetAllProjectIds(CancellationToken cancellationToken)
    {
        var projectIds = await _projectsService.GetProjectIdsAsync(userLimitation: null, cancellationToken);

        return projectIds.Any()
            ? Ok(projectIds)
            : NoContent();
    }

    [HttpGet]
    [Authorize(RoleSystem.User)]
    public async Task<ActionResult<Guid[]>> GetMyProjectIds(CancellationToken cancellationToken)
    {
        var userLimitation = User.GetUsername();

        var projectIds = await _projectsService.GetProjectIdsAsync(userLimitation, cancellationToken);

        return projectIds.Any()
            ? Ok(projectIds)
            : NoContent();
    }

    [HttpGet("{id:guid}")]
    [Authorize(RoleSystem.User)]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id, CancellationToken cancellationToken)
    {
        var userLimitation = User.GetRole() is not RoleSystem.Admin and not RoleSystem.Manager
            ? User.GetUsername()
            : null;

        var project = await _projectsService.GetProjectAsync(id, userLimitation, cancellationToken);

        return project is not null
            ? Ok(project)
            : NotFound();
    }

    [HttpPost]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> CreateProject(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var projectId = await _projectsService.CreateProjectAsync(request, cancellationToken);

        return Accepted(projectId);
    }

    [HttpPut]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> UpdateProject(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        await _projectsService.UpdateProjectAsync(request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
    {
        await _projectsService.DeleteProjectAsync(id, cancellationToken);

        return NoContent();
    }
}