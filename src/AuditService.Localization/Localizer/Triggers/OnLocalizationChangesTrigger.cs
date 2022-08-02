using AuditService.Common.Models.Domain;
using AuditService.Localization.Localizer.Models;
using AuditService.Localization.Localizer.Storage;

namespace AuditService.Localization.Localizer.Triggers;

/// <summary>
///     Trigger on localization changes
/// </summary>
internal class OnLocalizationChangesTrigger : IOnLocalizationChangesTrigger
{
    private readonly ILocalizationStorage _localizationStorage;

    /// <summary>
    ///     Non-language fields to be ignored
    /// </summary>
    private static readonly List<string> FieldsToIgnore = new() { "name", "uuid" };

    public OnLocalizationChangesTrigger(ILocalizationStorage localizationStorage)
    {
        _localizationStorage = localizationStorage;
    }

    /// <summary>
    ///     Push localization changes
    /// </summary>
    /// <param name="model">Localization changed model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task execution result</returns>
    public async Task PushChangesAsync(LocalizationChangedDomainModel model, CancellationToken cancellationToken = default)
    {
        var service = GenerateService(model);

        foreach (var language in SelectAllLanguages(model))
            await _localizationStorage.ClearResources(new LocalizationResourceParameters(service, language), cancellationToken);
    }

    /// <summary>
    /// Select all localization languages
    /// </summary>
    /// <param name="model">Localization changed model</param>
    /// <returns>Localization languages</returns>
    private static IEnumerable<string> SelectAllLanguages(LocalizationChangedDomainModel model) =>
        model.Text.SelectMany(changedFields => changedFields.Keys).Distinct().Where(field => !FieldsToIgnore.Contains(field));

    /// <summary>
    ///     Generate service for localization
    /// </summary>
    /// <param name="model">Localization changed model</param>
    /// <returns>Service name</returns>
    private static string GenerateService(LocalizationChangedDomainModel model) =>
        $"{model.Service}-{model.Type}";
}