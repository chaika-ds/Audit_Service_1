using Tolar.Authenticate.Impl;

namespace AuditService.Setup.ConfigurationSettings;

/// <summary>
///     Configuration section of SSO
/// </summary>
public interface IAuthSsoServiceSettings : IAuthenticateServiceSettings
{
    /// <summary>
    ///     Service name in SSO
    /// </summary>
    public string ServiceName { get; }
}