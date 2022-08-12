using AuditService.SettingsService.ApiClient;
using AuditService.SettingsService.Commands.BaseEntities;
using AuditService.SettingsService.Commands.GetRootNodeTree;
using AuditService.SettingsService.Settings;
using AuditService.SettingsService.Settings.Interfaces;
using AuditService.SettingsService.Storage;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Refit;

namespace AuditService.SettingsService;

/// <summary>
///     Settings service configurator
/// </summary>
public static class SettingsServiceConfigurator
{
    /// <summary>
    ///     Configure settings service. Register services and settings.
    /// </summary>
    /// <param name="services">Services сollection</param>
    public static void ConfigureSettingsService(this IServiceCollection services)
    {
        services.RegisterSettings();
        services.RegisterServices();
    }

    /// <summary>
    ///     Register settings
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterSettings(this IServiceCollection services)
    {
        services.AddSettings<ISettingsService, Settings.SettingsService>();
        services.AddSettings<IRedisCacheStorageSettings, RedisCacheStorageSettings>();
    }

    /// <summary>
    ///     Register services
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterServices(this IServiceCollection services)
    {
        services.RegisterApiClient();
        services.AddScoped<ISettingsServiceStorage, RedisCacheStorage>();
        services.AddScoped<IGetRootNodeTreeCommand, GetRootNodeTreeCommand>();
        services.AddScoped<SettingsServiceCommands>();
    }

    /// <summary>
    ///     Register API client
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterApiClient(this IServiceCollection services)
    {
        services.AddTransient<AuthHeaderHandler>();
        services.AddRefitClient<ISettingsServiceApiClient>(new RefitSettings
        {
            ContentSerializer = new NewtonsoftJsonContentSerializer()
        })
        .ConfigureHttpClient((serviceProvider, client) =>
        {
            var settings = serviceProvider.GetRequiredService<ISettingsService>();
            client.BaseAddress = new Uri(settings.Url);
        }).AddHttpMessageHandler<AuthHeaderHandler>();
    }
}