using AuditService.WebApi.Models;
using AuditService.WebApi.Services.Interfaces;

namespace AuditService.WebApi.Services;

public class AuditLogService : IAuditLog
{
    public IEnumerable<KafkaMessage> GetMockedLog()
    {
        var kafkaMessages = new List<KafkaMessage>();

        for (var i = 0; i < 10; i++)
        {
            KafkaMessage kafkaMessage = new KafkaMessage()
            {
                ServiceName = $"Service Name {i}",
                NodeId = Guid.NewGuid(),
                NodeType = $"Node Type {i}",
                ActionName = $"Action Name {i}",
                CategoryCode = $"Category Code {i}",
                RequestUrl = $"Request Url {i}",
                RequestBody = $"Request body {i}",
                Timestamp = DateTime.UtcNow,
                EntityName = $"Entity Name {i}",
                EntityId =  $"Entity Id {i}",
                OldValue = $"Old Value {i}",
                NewValue = $"New Value {i}",
                ProjectId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserIp = $"User Ip {i}",
                UserLogin = $"User Login {i}",
                UserAgent = $"User Agent {i}"
            };
            kafkaMessages.Add(kafkaMessage);
        }

        return kafkaMessages;
    }
}