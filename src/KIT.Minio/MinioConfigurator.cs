using bgTeam.Extensions;
using KIT.Minio.Commands.SaveFileWithSharing;
using KIT.Minio.Settings;
using KIT.Minio.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Tolar.MinioService.Client;
using Tolar.MinioService.Client.Impl;

namespace KIT.Minio;

/// <summary>
///     Minio configurator
/// </summary>
public static class MinioConfigurator
{
    /// <summary>
    ///     Configure minio. Register services and settings.
    /// </summary>
    /// <param name="services">Services сollection</param>
    public static void ConfigureMinio(this IServiceCollection services)
    {
       services.RegisterSettings();
       services.RegisterServices();
    }

    /// <summary>
    /// Register settings
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterSettings(this IServiceCollection services)
    {
        services.AddSettings<IFileStorageSettings, MinioSettings>();
        services.AddSettings<IMinioBucketSettings, MinioBucketSettings>();
        services.AddSettings<IMinioSharingFilesSettings, MinioSharingFilesSettings>();
    }

    /// <summary>
    /// Register services
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient<IFileStorageService, MinioServiceClient>();
        services.AddTransient<ISaveFileWithSharingCommand, SaveFileWithSharingCommand>();
    }
}