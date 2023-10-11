namespace IdentityServer.Data.Seeders;

public interface ISeeder
{
    public Task Run();
}

public static class SeederExtension
{
    public static async void UseSeeder<T>(this WebApplication application) where T : ISeeder
    {
        await using var scope = application.Services.CreateAsyncScope();
        var seeder = scope.ServiceProvider.GetRequiredService<T>();
        await seeder.Run();
    }
}