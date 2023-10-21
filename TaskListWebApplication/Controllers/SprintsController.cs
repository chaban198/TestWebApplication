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

    [HttpGet]
    [Authorize(RoleSystem.User)]
    public async Task<IActionResult> GetSprintFile(Guid sprintId, string fileName, CancellationToken cancellationToken)
    {
        var userLimitation = User.GetRole() is not RoleSystem.Admin and not RoleSystem.Manager
            ? User.GetUsername()
            : null;

        return await _sprintsService.GetFileOfSprintAsync(sprintId, fileName, userLimitation, cancellationToken);
    }

    [HttpPost]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> UploadSprintFile(Guid sprintId, IFormFile file, CancellationToken cancellationToken)
    {
        await _sprintsService.UploadFileToSprintAsync(sprintId, file, cancellationToken);
        return Accepted();
    }


    [HttpDelete]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> DeleteSprintFile(Guid sprintId, string fileName, CancellationToken cancellationToken)
    {
        await _sprintsService.DeleteFileOfSprintAsync(sprintId, fileName, cancellationToken);
        return NoContent();
    }
}