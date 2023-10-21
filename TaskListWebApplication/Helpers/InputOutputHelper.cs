namespace TaskListWebApplication.Helpers;

public static class InputOutputHelper
{
    private const string DefaultMimeType = "application/octet-stream";
    private static readonly Dictionary<string, string> MimeTypes = new()
    {
        { ".pdf", "application/pdf" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".png", "image/png" },
        { ".gif", "image/gif" },
        { ".bmp", "image/bmp" },
        { ".svg", "image/svg+xml" },
        { ".txt", "text/plain" },
        { ".html", "text/html" },
        { ".css", "text/css" },
        { ".js", "text/javascript" },
        { ".json", "application/json" },
        { ".xml", "application/xml" },
        { ".doc", "application/msword" },
        { ".xls", "application/vnd.ms-excel" },
        { ".ppt", "application/vnd.ms-powerpoint" },
        { ".zip", "application/zip" },
        { ".mp3", "audio/mpeg" },
        { ".mp4", "video/mp4" },
        { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        { ".ogg", "application/ogg" }
    };

    public static string GetContentType(string fileName)
    {
        var extension = Path.GetExtension(fileName);

        return MimeTypes.TryGetValue(extension, out var mimeType)
            ? mimeType
            : DefaultMimeType;
    }
}