namespace TaskListWebApplication.Helpers;

public class SwaggerUiAuthorizationCorrectorMiddleware : IMiddleware
{
    /// <summary>
    /// Добавляет приставку "Bearer" к токену в заголовке Authorization
    /// </summary>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authorization = context.Request.Headers.Authorization.FirstOrDefault();

        if (authorization?.Contains("Bearer") is false)
            context.Request.Headers.Authorization = $"Bearer {authorization}";

        await next.Invoke(context);
    }
}