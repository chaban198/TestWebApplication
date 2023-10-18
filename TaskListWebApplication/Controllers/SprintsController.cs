using Microsoft.AspNetCore.Mvc;
using TaskListWebApplication.Services;

namespace TaskListWebApplication.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class SprintsController : ControllerBase
{
    private readonly ISprintsService _sprintsService;

    public SprintsController(ISprintsService sprintsService)
    {
        _sprintsService = sprintsService;
    }
}