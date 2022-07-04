using Microsoft.Extensions.Configuration;

namespace AuditService.Kafka.AppSetings;

/// <summary>
///     Permission pusher interface to SSO
/// </summary>
public class PermissionPusherSettings : IPermissionPusherSettings
{
    public PermissionPusherSettings(IConfiguration configuration) => ApplySettings(configuration);

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

    /// <summary>
    ///     Apply configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        ServiceId = Guid.Parse(config["SSO:ServiceId"] ?? throw new InvalidOperationException("Wrong ServiceId."));
        ServiceName = config["SSO:ServiceName"];
        Topic = config["Kafka:Topics:PermissionsTopic"];
    }
}