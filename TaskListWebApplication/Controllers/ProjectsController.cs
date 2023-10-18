using GlobalDomain.Models.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;
using TaskListWebApplication.Services;

namespace TaskListWebApplication.Controllers;

[ApiController]
[Route("[controller]/[action]")]
[Authorize(RoleSystem.Manager)]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;
    private readonly IUrlHelper _urlHelper;

    public ProjectsController(IProjectsService projectsService, IUrlHelper urlHelper)
    {
        _projectsService = projectsService;
        _urlHelper = urlHelper;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ProjectDto>> GetProject(Guid id, CancellationToken cancellationToken)
    {
        var project = await _projectsService.GetProjectAsync(id, cancellationToken);

        return project is not null
            ? Ok(project)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateProject(CreateProjectRequest request, CancellationToken cancellationToken)
    {
        var projectId = await _projectsService.CreateProjectAsync(request, cancellationToken);

        var url = _urlHelper.ActionLink(nameof(GetProject), nameof(ProjectsController), projectId);

        return url is not null
            ? Accepted(new Uri(url))
            : Accepted();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateProject(UpdateProjectRequest request, CancellationToken cancellationToken)
    {
        await _projectsService.UpdateProjectAsync(request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteProject(Guid id, CancellationToken cancellationToken)
    {
        await _projectsService.DeleteProjectAsync(id, cancellationToken);

        return NoContent();
    }
}