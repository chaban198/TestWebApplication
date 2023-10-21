using TaskListWebApplication.Models.Infrastructure;

namespace TaskListWebApplication.Services;

public interface IFilesStorage
{
    Task<byte[]> GetFileAsync(StorageFileInfo fileInfo, CancellationToken cancellationToken);

    Task<string> UploadFileAsync(StorageFileInfo fileInfo, IFormFile file, CancellationToken cancellationToken = default);

    Task RemoveFileAsync(StorageFileInfo fileInfo, CancellationToken cancellationToken = default);

    Task RemoveFilesScopeAsync(string scopeName, Guid scopeId, CancellationToken cancellationToken = default);
}