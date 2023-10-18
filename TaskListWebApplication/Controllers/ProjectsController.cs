using Microsoft.AspNetCore.Mvc;
using TaskListWebApplication.Services;

namespace TaskListWebApplication.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsService _projectsService;

    public ProjectsController(IProjectsService projectsService)
    {
        _projectsService = projectsService;
    }
}