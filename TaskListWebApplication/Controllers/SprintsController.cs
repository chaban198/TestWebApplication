using Microsoft.AspNetCore.Mvc;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;
using TaskListWebApplication.Services;

namespace TaskListWebApplication.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class SprintsController : ControllerBase
{
    private readonly ISprintsService _sprintsService;
    private readonly IUrlHelper _urlHelper;

    public SprintsController(ISprintsService sprintsService, IUrlHelper urlHelper)
    {
        _sprintsService = sprintsService;
        _urlHelper = urlHelper;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<SprintDto>> GetSprint(Guid id, CancellationToken cancellationToken)
    {
        var sprint = await _sprintsService.GetSprintAsync(id, cancellationToken);

        return sprint is not null
            ? Ok(sprint)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateSprint(CreateSprintRequest request, CancellationToken cancellationToken)
    {
        var sprintId = await _sprintsService.CreateSprintAsync(request, cancellationToken);

        var url = _urlHelper.ActionLink(nameof(GetSprint), nameof(SprintsController), sprintId);

        return url is not null
            ? Accepted(new Uri(url))
            : Accepted();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateSprint(UpdateSprintRequest request, CancellationToken cancellationToken)
    {
        await _sprintsService.UpdateSprintAsync(request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteSprint(Guid id, CancellationToken cancellationToken)
    {
        await _sprintsService.DeleteSprintAsync(id, cancellationToken);

        return NoContent();
    }
}