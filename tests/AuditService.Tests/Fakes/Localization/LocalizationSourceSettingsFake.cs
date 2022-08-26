using AuditService.Localization.Settings;

namespace AuditService.Tests.Fakes.Localization;

/// <summary>
///      LocalizationSourceSettings fake
/// </summary>
public class LocalizationSourceSettingsFake: ILocalizationSourceSettings
{
    /// <summary>
    ///     Uri template for downloading localization resources
    /// </summary>
    public string? UriTemplate { 
        get => ""; 
        set => throw new NotImplementedException();
    }
}