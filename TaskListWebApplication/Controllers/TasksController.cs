using Microsoft.AspNetCore.Mvc;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;
using TaskListWebApplication.Services;

namespace TaskListWebApplication.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IUserTasksService _userTasksService;
    private readonly IUrlHelper _urlHelper;

    public TasksController(IUserTasksService userTasksService, IUrlHelper urlHelper)
    {
        _userTasksService = userTasksService;
        _urlHelper = urlHelper;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UserTaskDto>> GetUserTask(Guid id, CancellationToken cancellationToken)
    {
        var userTask = await _userTasksService.GetUserTaskAsync(id, cancellationToken);

        return userTask is not null
            ? Ok(userTask)
            : NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUserTask(CreateUserTaskRequest request, CancellationToken cancellationToken)
    {
        var userTaskId = await _userTasksService.CreateUserTaskAsync(request, cancellationToken);

        var url = _urlHelper.ActionLink(nameof(GetUserTask), nameof(TasksController), userTaskId);

        return url is not null
            ? Accepted(new Uri(url))
            : Accepted();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserTask(UpdateUserTaskRequest request, CancellationToken cancellationToken)
    {
        await _userTasksService.UpdateUserTaskAsync(request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteUserTask(Guid id, CancellationToken cancellationToken)
    {
        await _userTasksService.DeleteUserTask(id, cancellationToken);

        return NoContent();
    }
}