using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

/// <summary>
///     Settings for authentication in the SSO service
/// </summary>
internal class AuthSsoServiceSettings : IAuthSsoServiceSettings
{
    public AuthSsoServiceSettings(IConfiguration config)
    {
        ApiKey = config["SSO:ApiKey"];
        Connection = config["SSO:Url"];
        ServiceName = config["SSO:ServiceName"];
        ServiceId = Guid.Parse(config["SSO:ServiceId"] ?? throw new InvalidOperationException("Wrong ServiceId."));
        RootNodeId = Guid.Parse(config["SSO:RootNodeId"] ?? throw new InvalidOperationException("Wrong RootNodeId."));
    }

    /// <summary>
    ///     Service name
    /// </summary>
    public string ServiceName { get; }

    /// <summary>
    ///     Connection to SSO
    /// </summary>
    public string? Connection { get; }

    /// <summary>
    ///     Service Id
    /// </summary>
    public Guid ServiceId { get; }

    /// <summary>
    ///     Api key
    /// </summary>
    public string? ApiKey { get; }

    /// <summary>
    ///     Root node Id
    /// </summary>
    public Guid RootNodeId { get; }
}