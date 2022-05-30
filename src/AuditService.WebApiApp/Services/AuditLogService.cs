using AuditService.Data.Domain.Dto;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.Services.Interfaces;

namespace AuditService.WebApiApp.Services;

public class AuditLogService : IAuditLog
{
    public IEnumerable<AuditLogMessageDto> GetMockedLog()
    {
        var kafkaMessages = new List<AuditLogMessageDto>();

        for (var i = 0; i < 10; i++)
        {
            var kafkaMessage = new AuditLogMessageDto()
            {
                ServiceName = ServiceName.SETTINGSERVICE,
                NodeId = Guid.NewGuid(),
                NodeType = NodeTypes.ROOT,
                ActionName = ActionNameType.CREATE,
                CategoryCode = $"Category Code {i}",
                RequestUrl = $"Request Url {i}",
                RequestBody = $"Request body {i}",
                Timestamp = DateTime.UtcNow,
                EntityName = $"Entity Name {i}",
                EntityId = Guid.NewGuid(),
                OldValue = $"Old Value {i}",
                NewValue = $"New Value {i}",
                ProjectId = Guid.NewGuid(),
                User = new IdentityUserDto
                {
                    Id = Guid.NewGuid(),
                    Ip = $"User Ip {i}",
                    Login = $"User Login {i}",
                    UserAgent = $"User Agent {i}"
                }
            };
            kafkaMessages.Add(kafkaMessage);
        }

        return kafkaMessages;
    }
}