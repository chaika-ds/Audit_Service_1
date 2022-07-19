using AuditService.Localization.Localizer.Models;

namespace AuditService.Localization.Localizer;

/// <summary>
///     Application localizer.
///     Able to perform localization by keys/key.
/// </summary>
public interface ILocalizer
{
    /// <summary>
    ///     Try to localize
    ///     If no value is found for the key, the key will be returned
    /// </summary>
    /// <param name="request">Request for localization by keywords</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localized keys</returns>
    Task<IDictionary<string, string>> TryLocalize(LocalizeKeysRequest request, CancellationToken cancellationToken);

    /// <summary>
    ///     Try to localize
    ///     If no value is found for the key, the key will be returned
    /// </summary>
    /// <param name="request">Request for localization by key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localized key</returns>
    Task<string> TryLocalize(LocalizeKeyRequest request, CancellationToken cancellationToken);
}