using AuditService.Localization.Localizer;
using AuditService.Localization.Localizer.Source;
using AuditService.Localization.Localizer.Storage;
using AuditService.Localization.Settings;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Localization;

/// <summary>
///     Localization configurator
/// </summary>
public static class LocalizationConfigurator
{
    /// <summary>
    ///     Configure Localization. Register services and settings.
    /// </summary>
    /// <param name="services">Services сollection</param>
    public static void ConfigureLocalization(this IServiceCollection services)
    {
        services.AddSettings<IRedisCacheStorageSettings, RedisCacheStorageSettings>();
        services.AddSettings<ILocalizationSourceSettings, LocalizationSourceSettings>();
        services.AddScoped<ILocalizationStorage, RedisCacheStorage>();
        services.AddScoped<ILocalizationSource, LocalizationService>();
        services.AddScoped<ILocalizer, Localizer.Localizer>();
    }
}