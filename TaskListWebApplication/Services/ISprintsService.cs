using Microsoft.AspNetCore.Mvc;
using TaskListWebApplication.Models.Api;
using TaskListWebApplication.Models.Dto;

namespace TaskListWebApplication.Services;

public interface ISprintsService
{
    public const string StaticFilesScope = "sprint";

    Task<SprintDto?> GetSprintAsync(Guid id, string? userLimitation, CancellationToken cancellationToken = default);

    Task<SprintDto[]> GetSprintsByProjectIdAsync(Guid projectId, string? userLimitation, CancellationToken cancellationToken = default);

    Task<Guid> CreateSprintAsync(CreateSprintRequest request, CancellationToken cancellationToken = default);

    Task UpdateSprintAsync(UpdateSprintRequest request, CancellationToken cancellationToken = default);

    Task DeleteSprintAsync(Guid id, CancellationToken cancellationToken = default);

    Task UploadFileToSprintAsync(Guid sprintId, IFormFile file, CancellationToken cancellationToken = default);

    Task<FileContentResult> GetFileOfSprintAsync(Guid sprintId, string fileName, string? userLimitation, CancellationToken cancellationToken = default);

    Task DeleteFileOfSprintAsync(Guid sprintId, string fileName, CancellationToken cancellationToken = default);
}