namespace TaskListWebApplication.Models.Dto;

public readonly struct UsersCheckResult
{
    public bool IsValid => NotExistedUsers.Any() is not true;
    public required string[] NotExistedUsers { get; init; }

    public override string ToString() => IsValid
        ? "Пользователи прошли проверку"
        : $"Пользователи {string.Join(", ", NotExistedUsers)} не найдены";
}