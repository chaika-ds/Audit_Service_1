using AuditService.Tests.TRASH.Interfaces;

namespace AuditService.Tests.TRASH.Models;

/// <summary>
///     SSO Mock model
/// </summary>
public class SsoMock : IBaseMock
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