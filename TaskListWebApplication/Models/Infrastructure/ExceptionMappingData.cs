using System.Net;

namespace TaskListWebApplication.Models.Infrastructure;

public readonly struct ExceptionMappingData
{
    public int Code { get; init; }
    public string Message { get; init; }

    #region Static Builders

    public static ExceptionMappingData InternalError => new()
    {
        Code = (int)HttpStatusCode.InternalServerError,
        Message = "Internal Server Error"
    };

    #endregion
}