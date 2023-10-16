namespace TaskListWebApplication.Models.Other;

public readonly struct LockoutCacheState
{
    public bool IsLocked { get; init; }
    public DateTime CheckMoment { get; init; }

    public bool IsActual(TimeSpan expire) => CheckMoment + expire > DateTime.Now;
}