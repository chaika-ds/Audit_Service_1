using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

internal class PermissionPusherSettings : IPermissionPusherSettings
{
    public PermissionPusherSettings(IConfiguration configuration) => ApplySettings(configuration);

    public string? Topic { get; set; }

    public Guid ServiceId { get; set; }

    public string? ServiceName { get; set; }

    /// <summary>
    ///     Apply configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        ServiceId = Guid.Parse(config["SSO:ServiceId"] ?? throw new InvalidOperationException("Wrong ServiceId."));
        ServiceName = config["SSO:ServiceName"];
        Topic = config["Kafka:PermissionsTopic"];
    }
}