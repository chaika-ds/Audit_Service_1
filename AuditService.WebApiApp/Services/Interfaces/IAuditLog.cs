using AuditService.Data.Domain.Dto;

namespace AuditService.WebApiApp;

public interface IAuditLog
{
    IEnumerable<AuditLogMessageDto> GetMockedLog();
}