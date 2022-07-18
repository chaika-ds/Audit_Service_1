using AuditService.Common.Models.Domain.AuditLog;

namespace KIT.Kafka.Consumers.AuditLog;

/// <summary>
/// Audit log consumer message
/// </summary>
public class AuditLogConsumerMessage : BaseAuditLogModel
{
    /// <summary>
    ///     Node Name
    /// </summary>
    public string NodeName { get; set; }
}