using AuditService.Common.Models.Domain.PlayerChangesLog;
using KIT.Kafka.Consumers.PlayerChangesLog;

namespace AuditService.Tests.KIT.Kafka.TestModels
{
    internal class PlayerChangesLogValidatorTestModels
    {
        internal static PlayerChangesLogConsumerMessage GetPlayerChangesLogConsumerMessage()
        {
            return new PlayerChangesLogConsumerMessage()
            {
                NodeId = Guid.Empty,
                ProjectId = Guid.Empty,
                EventCode = null,
                Timestamp = new DateTime(),
                PlayerId = Guid.Empty,
                IpAddress = null,
                User = null,
                OldValues = new Dictionary<string, PlayerAttributeDomainModel>(),
                NewValues = new Dictionary<string, PlayerAttributeDomainModel>()
            };
        }

        internal static UserInitiatorDomainModel GetUserInitiatorDomainModel()
        {
            return new UserInitiatorDomainModel()
            {
                Email = null,
                Id = Guid.Empty,
                UserAgent = null
            };
        }
    }
}
