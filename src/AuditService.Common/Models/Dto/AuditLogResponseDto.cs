using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.AuditLog;

namespace AuditService.Common.Models.Dto;

/// <summary>
///     Audit log response model
/// </summary>
public class AuditLogResponseDto : AuditLogDomainModel
{
    /// <summary>
    ///     Node type
    /// </summary>
    public NodeType? NodeType { get; set; }
}