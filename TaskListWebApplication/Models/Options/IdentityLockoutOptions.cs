namespace TaskListWebApplication.Models.Options;

public class IdentityLockoutOptions
{
    public bool LockoutEnabled { get; init; }
    public TimeSpan LockoutCacheExpire { get; init; }
}