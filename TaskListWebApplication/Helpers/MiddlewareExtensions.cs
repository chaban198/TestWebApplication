namespace TaskListWebApplication.Helpers;

public static class MiddlewareExtensions
{
    /// <summary>
    /// Добавляет приставку "Bearer" к токену в заголовке Authorization
    /// </summary>
    public static void UseSwaggerUiAuthorizationCorrector(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<SwaggerUiAuthorizationCorrectorMiddleware>();
    }
}