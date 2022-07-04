namespace AuditService.Kafka.AppSetings;

/// <summary>
///     Permission pusher interface to SSO
/// </summary>
public interface IPermissionPusherSettings
{
    /// <summary>
    ///     Topic of Kafka
    /// </summary>
    public string? Topic { get; set; }

    /// <summary>
    ///     Service ID
    /// </summary>
    public Guid ServiceId { get; set; }

    /// <summary>
    ///     Service name
    /// </summary>
    public string? ServiceName { get; set; }
}