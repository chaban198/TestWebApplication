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
public class TasksController : ControllerBase
{
    private readonly IUserTasksService _userTasksService;

    public TasksController(IUserTasksService userTasksService)
    {
        _userTasksService = userTasksService;
    }

    [HttpGet]
    [Authorize(RoleSystem.User)]
    public async Task<ActionResult<Guid[]>> GetAllMyTaskIds(CancellationToken cancellationToken)
    {
        var username = User.GetUsername();

        var userTask = await _userTasksService.GetAllUserTaskIdsAsync(username, cancellationToken);

        return userTask.Any()
            ? Ok(userTask)
            : NoContent();
    }

    [HttpGet("{id:guid}")]
    [Authorize(RoleSystem.User)]
    public async Task<ActionResult<UserTaskDto>> GetTask(Guid id, CancellationToken cancellationToken)
    {
        var userLimitation = User.GetRole() is not RoleSystem.Admin and not RoleSystem.Manager
            ? User.GetUsername()
            : null;

        var userTask = await _userTasksService.GetUserTaskAsync(id, userLimitation, cancellationToken);

        return userTask is not null
            ? Ok(userTask)
            : NotFound();
    }

    [HttpPost]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> CreateTask(CreateUserTaskRequest request, CancellationToken cancellationToken)
    {
        var userTaskId = await _userTasksService.CreateUserTaskAsync(request, cancellationToken);

        return Accepted(userTaskId);
    }

    [HttpPut]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> UpdateTask(UpdateUserTaskRequest request, CancellationToken cancellationToken)
    {
        await _userTasksService.UpdateUserTaskAsync(request, cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    [Authorize(RoleSystem.Manager)]
    public async Task<IActionResult> DeleteTask(Guid id, CancellationToken cancellationToken)
    {
        await _userTasksService.DeleteUserTask(id, cancellationToken);

        return NoContent();
    }
}