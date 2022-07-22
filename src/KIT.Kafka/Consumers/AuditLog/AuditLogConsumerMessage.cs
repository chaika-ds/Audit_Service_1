using System.ComponentModel.DataAnnotations;
using AuditService.Common.Models.Domain.AuditLog;

namespace KIT.Kafka.Consumers.AuditLog;

/// <summary>
/// Audit log consumer message
/// </summary>
public class AuditLogConsumerMessage : BaseAuditLogModel
{
    public AuditLogConsumerMessage()
    {
        NodeName = string.Empty;
    }

    /// <summary>
    ///     Node Name
    /// </summary>
    [Required]
    public string NodeName { get; set; }
}