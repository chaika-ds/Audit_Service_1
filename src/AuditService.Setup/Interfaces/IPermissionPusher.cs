namespace AuditService.Setup.Interfaces;

/// <summary>
///     Permission pusher interface to SSO
/// </summary>
public interface IPermissionPusher
{
    /// <summary>
    ///     TopicOfKafka of Kafka
    /// </summary>
    public string TopicOfKafka { get; set; }

    /// <summary>
    ///     Service ID
    /// </summary>
    public Guid ServiceIdentificator { get; set; }

    /// <summary>
    ///     Service name
    /// </summary>
    public string? ServiceName { get; set; }
}