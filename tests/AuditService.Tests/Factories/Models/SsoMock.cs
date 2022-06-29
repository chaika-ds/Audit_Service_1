using Tolar.Authenticate.Dtos;

namespace AuditService.Tests.Factories.Models;

/// <summary>
///     SSO Mock model
/// </summary>
public class SsoMock : BaseMock
{
    /// <summary>
    ///     SSO Token
    /// </summary>
    public string? Token { get; set; }
    
    /// <summary>
    ///     SSO NodeId
    /// </summary>
    public Guid NodeId { get; set; }

    /// <summary>
    ///     SSO Expected Object
    /// </summary>
    public object? ExpectedObject { get; set; }
}