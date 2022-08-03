using AuditService.Common.Models.Domain;

namespace AuditService.Localization.Localizer.Triggers;

/// <summary>
///     Trigger on localization changes
/// </summary>
public interface IOnLocalizationChangesTrigger
{
    /// <summary>
    ///     Push localization changes
    /// </summary>
    /// <param name="model">Localization changed model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    Task PushChangesAsync(LocalizationChangedDomainModel model, CancellationToken cancellationToken = default);
}