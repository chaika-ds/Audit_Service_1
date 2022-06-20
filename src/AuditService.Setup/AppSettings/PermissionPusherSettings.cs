using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

internal class PermissionPusherSettings : IPermissionPusherSettings
{
    public PermissionPusherSettings(IConfiguration configuration) => ApplySettings(configuration);

    public string? Topic { get; set; }

    public Guid ServiceId { get; set; }

    /// <summary>
    ///     Apply configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        ServiceId = Guid.Parse(config["SSO:SSO_AUTH_SERVICE_ID"] ?? throw new InvalidOperationException("Wrong ServiceId."));
        Topic = config["KAFKA:PermissionsTopic"];
    }
}