namespace KIT.Kafka.Settings.Interfaces;

/// <summary>
///     Permission pusher interface to SSO
/// </summary>
public interface IPermissionPusherSettings
{
    /// <summary>
    ///     Service ID
    /// </summary>
    public Guid ServiceId { get; set; }

    /// <summary>
    ///     Service name
    /// </summary>
    public string? ServiceName { get; set; }
}