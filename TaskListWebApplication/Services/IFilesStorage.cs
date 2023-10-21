using TaskListWebApplication.Models.Infrastructure;

namespace TaskListWebApplication.Services;

public interface IFilesStorage
{
    Task<byte[]> GetFileAsync(StaticFileInfo fileInfo, CancellationToken cancellationToken);

    Task<string> UploadFileAsync(StaticFileInfo fileInfo, IFormFile file, CancellationToken cancellationToken = default);

    Task RemoveFileAsync(StaticFileInfo fileInfo, CancellationToken cancellationToken = default);

    Task RemoveFilesScopeAsync(string scopeName, Guid scopeId, CancellationToken cancellationToken = default);
}