using Microsoft.AspNetCore.Mvc;
using TaskListWebApplication.Services;

namespace TaskListWebApplication.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class TasksController : ControllerBase
{
    private readonly IUserTasksService _userTasksService;

    public TasksController(IUserTasksService userTasksService)
    {
        _userTasksService = userTasksService;
    }
}