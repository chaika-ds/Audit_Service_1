using KIT.Minio.Commands.SaveFileWithSharing;
using KIT.Minio.Commands.SaveFileWithSharing.Models;

namespace AuditService.Tests.Fakes.Minio;

/// <summary>
///     Fake SaveFileWithSharingCommand
/// </summary>
internal class SaveFileWithSharingCommandFake : ISaveFileWithSharingCommand
{
    /// <summary>
    ///     Document from ExecuteAsync method
    /// </summary>
    public SaveFileWithSharingModel? FileStreamDocument { get; set; }

    /// <summary>
    ///     Fake saving of document
    /// </summary>
    /// <param name="request">SaveFileWithSharingModel</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns>String</returns>
    public Task<string> ExecuteAsync(SaveFileWithSharingModel request, CancellationToken cancellationToken = default)
    {
        FileStreamDocument = request;

        return Task.FromResult(string.Empty);
    }
}
