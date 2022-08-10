namespace KIT.Minio.Settings.Interfaces;

/// <summary>
///     Settings for sharing minio files
/// </summary>
public interface IMinioSharingFilesSettings
{
    /// <summary>
    ///     Shared file expiration in seconds
    /// </summary>
    int ExpirationInSeconds { get; set; }
}