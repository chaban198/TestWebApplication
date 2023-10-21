using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface IUsersService
{
    Task<UsersCheckResult> CheckUserAsync(string user);

    Task<UsersCheckResult> CheckUsersAsync(IEnumerable<string> users);
}