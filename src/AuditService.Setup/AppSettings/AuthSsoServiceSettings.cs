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
        ApiKey = config["SSO:ApiKey"];
        Connection = config["SSO:Url"];
        ServiceName = config["SSO:ServiceName"];
        ServiceId = Guid.Parse(config["SSO:ServiceId"] ?? throw new InvalidOperationException("Wrong ServiceId."));
        RootNodeId = Guid.Parse(config["SSO:RootNodeId"] ?? throw new InvalidOperationException("Wrong RootNodeId."));
    }
}