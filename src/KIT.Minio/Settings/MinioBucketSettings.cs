using KIT.Minio.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KIT.Minio.Settings;

/// <summary>
///     Minio bucket settings
/// </summary>
internal class MinioBucketSettings : IMinioBucketSettings
{
    public MinioBucketSettings(IConfiguration configuration)
    {
        BucketName = configuration["Minio:BucketName"];
    }

    /// <summary>
    ///     Bucket name
    /// </summary>
    public string BucketName { get; set; }
}