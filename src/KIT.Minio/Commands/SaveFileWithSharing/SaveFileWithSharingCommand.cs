using KIT.Minio.Commands.SaveFileWithSharing.Models;
using KIT.Minio.Settings.Interfaces;
using KIT.NLog.Extensions;
using Microsoft.Extensions.Logging;
using Tolar.MinioService.Client;

namespace KIT.Minio.Commands.SaveFileWithSharing;

/// <summary>
///     Command to save a file with sharing
/// </summary>
internal class SaveFileWithSharingCommand : ISaveFileWithSharingCommand
{
    private readonly IFileStorageService _fileStorageService;
    private readonly IMinioBucketSettings _minioBucketSettings;
    private readonly IMinioSharingFilesSettings _minioSharingFilesSettings;
    private readonly ILogger<SaveFileWithSharingCommand> _logger;

    public SaveFileWithSharingCommand(IFileStorageService fileStorageService, IMinioBucketSettings minioBucketSettings,
        IMinioSharingFilesSettings minioSharingFilesSettings, ILogger<SaveFileWithSharingCommand> logger)
    {
        _fileStorageService = fileStorageService;
        _minioBucketSettings = minioBucketSettings;
        _minioSharingFilesSettings = minioSharingFilesSettings;
        _logger = logger;
    }

    /// <summary>
    ///  Execute сommand to save a file with sharing
    /// </summary>
    /// <param name="request">Model to save file with sharing</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Link to shared file</returns>
    public async Task<string> ExecuteAsync(SaveFileWithSharingModel request, CancellationToken cancellationToken = default)
    {
        try
        {
            await _fileStorageService.SaveFileAsync(request.Stream, _minioBucketSettings.BucketName, request.FileName, request.ContentType);
            var fileStorageResponse = await _fileStorageService.GetTemporaryLinkAsync(_minioBucketSettings.BucketName, request.FileName, _minioSharingFilesSettings.ExpirationInSeconds);
            return fileStorageResponse.FileLink;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, contextModel: request);
            throw;
        }
    }
}