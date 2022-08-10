namespace KIT.Minio.Settings.Interfaces;

/// <summary>
///     Minio bucket settings
/// </summary>
public interface IMinioBucketSettings
{
    /// <summary>
    ///     Bucket name
    /// </summary>
    string BucketName { get; set; }
}