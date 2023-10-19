using Microsoft.EntityFrameworkCore;
using TaskListWebApplication.Data;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public class UsersService : IUsersService
{
    private readonly IdentityReadonlyDbContext _dbContext;

    public UsersService(IdentityReadonlyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<UsersCheckResult> CheckUser(string user) => CheckUsers(new[] { user });

    public async Task<UsersCheckResult> CheckUsers(IEnumerable<string> usersInput)
    {
        var users = usersInput.ToArray();

        var existedUsers = await _dbContext.Users
            .Where(x => users.Contains(x.UserName))
            .Select(x => x.UserName)
            .ToArrayAsync();

        string[] nonExistedUsers = users
            .Except(existedUsers)
            .Where(x => string.IsNullOrEmpty(x) is false)
            .ToArray()!;

        return new UsersCheckResult { NotExistedUsers = nonExistedUsers };
    }
}