using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace GlobalDomain.Infrastructure.Swagger;

public class SwaggerUiAuthorizationCorrectorMiddleware : IMiddleware
{
    /// <summary>
    /// Добавляет приставку "Bearer" к токену в заголовке Authorization
    /// </summary>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var authorization = context.Request.Headers.Authorization.FirstOrDefault();

        if (authorization?.StartsWith("Bearer") is false)
            context.Request.Headers.Authorization = $"Bearer {authorization}";

        await next.Invoke(context);
    }
}

public static class SwaggerUiAuthorizationCorrectorMiddlewareExtension
{
    /// <summary>
    /// Добавляет приставку "Bearer" к токену в заголовке Authorization
    /// </summary>
    public static void UseSwaggerUiAuthorizationCorrector(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<SwaggerUiAuthorizationCorrectorMiddleware>();
    }
}