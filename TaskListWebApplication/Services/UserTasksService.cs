namespace TaskListWebApplication.Services;

public class UserTasksService : IUserTasksService
{
    private readonly IUserTasksService _userTasksService;

    public UserTasksService(IUserTasksService userTasksService)
    {
        _userTasksService = userTasksService;
    }
}