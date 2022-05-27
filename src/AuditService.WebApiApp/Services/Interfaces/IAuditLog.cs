using AuditService.Data.Domain.Dto;

namespace AuditService.WebApiApp.Services.Interfaces;

public interface IAuditLog
{
    IEnumerable<AuditLogTransactionDto> GetMockedLog();
}