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
                NotFoundException => new ExceptionMappingData { Code = (int)HttpStatusCode.NotFound, Message = "Данные не найдены" },
                DataConflictException => new ExceptionMappingData { Code = (int)HttpStatusCode.Conflict, Message = "Ошибка согласованности данных" },
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

#region ExtensionHelper

public static class ExceptionHandlerMiddlewareExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app) => app.UseMiddleware<CustomExceptionHandlerMiddleware>();
}

#endregion