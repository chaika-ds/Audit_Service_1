using Tolar.Authenticate.Dtos;

namespace AuditService.Tests.Factories.Models;

public class SsoMock : BaseMock
{
    public string? Token { get; set; }
    public Guid NodeId { get; set; }
    public string? ExpectedToken { get; set; }
    public AuthenticatedResponse? ExpectedObject { get; set; }
}