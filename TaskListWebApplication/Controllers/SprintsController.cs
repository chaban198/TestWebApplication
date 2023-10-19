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
public class SprintsController : ControllerBase
{
    private readonly ISprintsService _sprintsService;

    public SprintsController(ISprintsService sprintsService)
    {
        _sprintsService = sprintsService;
    }

    [HttpGet("{id:guid}")]
    [Authorize(RoleSystem.User)]
    public async Task<ActionResult<SprintDto>> GetSprint(Guid id, CancellationToken cancellationToken)
    {
        var userLimitation = User.GetRole() is not RoleSystem.Admin and not RoleSystem.Manager
            ? User.GetUsername()
            : null;

        var sprint = await _sprintsService.GetSprintAsync(id, userLimitation, cancellationToken);

        return sprint is not null
            ? Ok(sprint)
            : NotFound();
    }

    [HttpGet("{projectId:guid}")]
    [Authorize(RoleSystem.User)]
    public async Task<ActionResult<SprintDto[]>> GetProjectSprints(Guid projectId, CancellationToken cancellationToken)
    {
        var userLimitation = User.GetRole() is not RoleSystem.Admin and not RoleSystem.Manager
            ? User.GetUsername()
            : null;

        var sprints = await _sprintsService.GetSprintsByProjectIdAsync(projectId, userLimitation, cancellationToken);

        return sprints.Any()
            ? Ok(sprints)
            : NoContent();
    }

    [HttpPost]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> CreateSprint(CreateSprintRequest request, CancellationToken cancellationToken)
    {
        var sprintId = await _sprintsService.CreateSprintAsync(request, cancellationToken);

        return Accepted(sprintId);
    }

    [HttpPut]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> UpdateSprint(UpdateSprintRequest request, CancellationToken cancellationToken)
    {
        await _sprintsService.UpdateSprintAsync(request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> DeleteSprint(Guid id, CancellationToken cancellationToken)
    {
        await _sprintsService.DeleteSprintAsync(id, cancellationToken);

        return NoContent();
    }
}