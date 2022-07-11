using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;

namespace KIT.Kafka.Consumers.AuditLog
{
    /// <summary>
    /// Audit log consumer message
    /// </summary>
    public class AuditLogConsumerMessage : BaseAuditLogModel
    {
        /// <summary>
        ///     Module Name
        /// </summary>
        public ServiceStructure ModuleName { get; set; }

        /// <summary>
        ///     Node Name
        /// </summary>
        public string NodeName { get; set; }
    }
}
