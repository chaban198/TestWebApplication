using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using GlobalDomain.Models.Exceptions;
using TaskListWebApplication.Models.Infrastructure;

namespace TaskListWebApplication.Infrastructure.Middlewares;

public class CustomExceptionHandlerMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            var exceptionMappingData = exception switch
            {
                AuthenticationException => new ExceptionMappingData { Code = (int)HttpStatusCode.Unauthorized, Message = "Ошибка аутентификации" },
                AuthenticationLockoutException => new ExceptionMappingData { Code = (int)HttpStatusCode.Forbidden, Message = "Пользователь заблокирован" },
                _ => ExceptionMappingData.InternalError
            };

            context.Response.Clear();
            context.Response.StatusCode = exceptionMappingData.Code;
            context.Response.ContentType = "application/json";

            var response = new
            {
                MainMessage = exceptionMappingData.Message,
                ExceptionMessage = exception.Message
            };

            var jsonResponse = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
    }
}