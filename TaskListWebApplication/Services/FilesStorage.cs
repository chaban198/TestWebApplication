using TaskListWebApplication.Models.Infrastructure;

namespace TaskListWebApplication.Services;

public class FilesStorage : IFilesStorage
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    private string FileStorageRoot => _hostingEnvironment.WebRootPath;

    public FilesStorage(IWebHostEnvironment hostingEnvironment)
    {
        _hostingEnvironment = hostingEnvironment;
    }

    public async Task<string> UploadFileAsync(StaticFileInfo fileInfo, IFormFile file, CancellationToken cancellationToken)
    {
        if (file is null) throw new ArgumentNullException(nameof(file));

        var fullPath = Path.Combine(FileStorageRoot, fileInfo.FilePath);
        var fileName = Path.GetFileName(fullPath);
        var directory = Path.GetDirectoryName(fullPath)!;

        Directory.CreateDirectory(directory);

        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return fileName;
    }

    public async Task<byte[]> GetFileAsync(StaticFileInfo fileInfo, CancellationToken cancellationToken)
    {
        var fullPath = Path.Combine(FileStorageRoot, fileInfo.FilePath);

        return await File.ReadAllBytesAsync(fullPath, cancellationToken);
    }

    public Task RemoveFileAsync(StaticFileInfo fileInfo, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var fullPath = Path.Combine(FileStorageRoot, fileInfo.FilePath);

        File.Delete(fullPath);

        return Task.CompletedTask;
    }

    public Task RemoveFilesScopeAsync(string scopeName, Guid scopeId, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var scopeDirectory = Path.Combine(FileStorageRoot, scopeName, scopeId.ToString());

        Directory.Delete(scopeDirectory);

        return Task.CompletedTask;
    }
}