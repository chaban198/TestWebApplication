using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface IUsersService
{
    Task<UsersCheckResult> CheckUser(string user);

    Task<UsersCheckResult> CheckUsers(IEnumerable<string> users);
}