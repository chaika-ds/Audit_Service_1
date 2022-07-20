using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Localization.Localizer.Consts;
using AuditService.Localization.Localizer.Models;
using AuditService.Localization.Localizer.Source;
using AuditService.Localization.Localizer.Storage;

namespace AuditService.Localization.Localizer;

/// <summary>
///     Application localizer.
///     Able to perform localization by keys/key.
/// </summary>
internal class Localizer : ILocalizer
{
    private readonly ILocalizationSource _localizationSource;
    private readonly ILocalizationStorage _localizationStorage;

    public Localizer(ILocalizationStorage localizationStorage, ILocalizationSource localizationSource)
    {
        _localizationStorage = localizationStorage;
        _localizationSource = localizationSource;
    }

    /// <summary>
    ///     Try to localize
    ///     If no value is found for the key, the key will be returned
    /// </summary>
    /// <param name="request">Request for localization by keywords</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localized keys</returns>
    public async Task<IDictionary<string, string>> TryLocalize(LocalizeKeysRequest request, CancellationToken cancellationToken)
    {
        var keys = request.Keys.Distinct();
        var resources = await GetLocalizationResources(CreateResourceParameters(request.Module, request.Language), cancellationToken);
        return keys.ToDictionary(key => key, key => resources.TryGetValue(key, out var value) ? value : key);
    }

    /// <summary>
    ///     Try to localize
    ///     If no value is found for the key, the key will be returned
    /// </summary>
    /// <param name="request">Request for localization by key</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localized key</returns>
    public async Task<string> TryLocalize(LocalizeKeyRequest request, CancellationToken cancellationToken)
    {
        var resources = await GetLocalizationResources(CreateResourceParameters(request.Module, request.Language), cancellationToken);
        return resources.TryGetValue(request.Key, out var value) ? value : request.Key;
    }

    /// <summary>
    ///     Get localization resources
    ///     Caches resources when needed
    /// </summary>
    /// <param name="localizationParameters">Localization resource parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Localization resources</returns>
    private async Task<IDictionary<string, string>> GetLocalizationResources(LocalizationResourceParameters localizationParameters, CancellationToken cancellationToken)
    {
        var resources = await _localizationStorage.GetResources(localizationParameters, cancellationToken);

        if (resources.Any())
            return resources;

        resources = await _localizationSource.LoadResources(localizationParameters, cancellationToken);
        await _localizationStorage.SetResources(new LocalizationResources(resources, localizationParameters), cancellationToken);
        return resources;
    }

    /// <summary>
    ///     Create localization resource parameters
    /// </summary>
    /// <param name="moduleName"> Identificator of service(Module name)</param>
    /// <param name="language">Translation language</param>
    /// <returns>Localization resource parameters</returns>
    private static LocalizationResourceParameters CreateResourceParameters(ModuleName moduleName, string? language) =>
        new(moduleName.LocalizationKey(), string.IsNullOrEmpty(language) ? LocalizerConst.DefaultLanguage : language);
}