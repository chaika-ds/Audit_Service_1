using AuditService.WebApi.Models;

namespace AuditService.WebApi.Services.Interfaces;

public interface IAuditLog
{
    IEnumerable<KafkaMessage> GetMockedLog();
}