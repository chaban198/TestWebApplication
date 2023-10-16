using System.Collections.Concurrent;
using System.Security.Authentication;
using GlobalDomain.Models.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskListWebApplication.Data;
using TaskListWebApplication.Helpers;
using TaskListWebApplication.Models.Options;
using TaskListWebApplication.Models.Other;

namespace TaskListWebApplication.Infrastructure.Middlewares;

public class AuthenticationLockoutMiddleware : IMiddleware
{
    private readonly IdentityReadonlyDbContext _db;
    private readonly IdentityLockoutOptions _options;

    private static readonly ConcurrentDictionary<string, LockoutCacheState> Cache = new();

    public AuthenticationLockoutMiddleware(IdentityReadonlyDbContext db, IOptions<IdentityLockoutOptions> options)
    {
        _db = db;
        _options = options.Value;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (_options.LockoutEnabled)
        {
            var isLocked = await GetUserLockout(context);

            if (isLocked)
                throw new AuthenticationLockoutException();
        }

        await next.Invoke(context);
    }

    private async Task<bool> GetUserLockout(HttpContext context)
    {
        var username = context.User.GetUsername();
        if (username is null)
            throw new AuthenticationException("Не удалось определить имя пользователя");

        if (Cache.TryGetValue(username, out var cacheState) && cacheState.IsActual(_options.LockoutCacheExpire))
            return cacheState.IsLocked;

        var user = await _db.Users.FirstOrDefaultAsync(x => x.UserName == username);
        if (user is null)
            throw new AuthenticationException($"Пользователь {username} не найден в базе данных");

        var isLocked = user.LockoutEnabled;

        var actualLockoutState = new LockoutCacheState
        {
            IsLocked = isLocked,
            CheckMoment = DateTime.Now
        };

        Cache.AddOrUpdate(username,
            _ => actualLockoutState,
            (_, _) => actualLockoutState);

        return isLocked;
    }
}

#region ExtensionHelper

public static class AuthenticationLockoutMiddlewareHelper
{
    public static void UseAuthenticationLockout(this WebApplication app) => app.UseMiddleware<AuthenticationLockoutMiddleware>();
}

#endregion