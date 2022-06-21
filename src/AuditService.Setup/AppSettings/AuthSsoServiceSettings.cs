using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

internal class AuthSsoServiceSettings : IAuthSsoServiceSettings
{
    public AuthSsoServiceSettings(IConfiguration configuration) => ApplySettings(configuration);

    public string? ServiceName { get; private set; }

    public string? Connection { get; private set; }

    public Guid ServiceId { get; private set; }

    public string? ApiKey { get; private set; }

    public Guid RootNodeId { get; private set; }

    /// <summary>
    ///     Apply SSO configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        ApiKey = config["SSO:SSO_AUTH_API_KEY"];
        Connection = config["SSO:SSO_SERVICE_URL"];
        ServiceName = config["SSO:SSO_SERVICE_NAME"];
        ServiceId = Guid.Parse(config["SSO:SSO_AUTH_SERVICE_ID"] ?? throw new InvalidOperationException("Wrong ServiceId."));
        RootNodeId = Guid.Parse(config["SSO:SSO_AUTH_ROOT_NODE_ID"] ?? throw new InvalidOperationException("Wrong RootNodeId."));
    }
}