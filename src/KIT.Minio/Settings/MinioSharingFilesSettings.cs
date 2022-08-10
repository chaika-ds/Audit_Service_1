using KIT.Minio.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KIT.Minio.Settings;

/// <summary>
///     Settings for sharing minio files
/// </summary>
internal class MinioSharingFilesSettings : IMinioSharingFilesSettings
{
    public MinioSharingFilesSettings(IConfiguration configuration)
    {
        ExpirationInSeconds = int.Parse(configuration["Minio:SharingFiles:ExpirationInSeconds"]);
    }

    /// <summary>
    ///     Shared file expiration in seconds
    /// </summary>
    public int ExpirationInSeconds { get; set; }
}