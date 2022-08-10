using KIT.Minio.Commands.SaveFileWithSharing.Models;

namespace KIT.Minio.Commands.SaveFileWithSharing;

/// <summary>
///     Command to save a file with sharing
/// </summary>
public interface ISaveFileWithSharingCommand
{
    /// <summary>
    ///  Execute сommand to save a file with sharing
    /// </summary>
    /// <param name="request">Model to save file with sharing</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Link to shared file</returns>
    Task<string> ExecuteAsync(SaveFileWithSharingModel request, CancellationToken cancellationToken = default);
}