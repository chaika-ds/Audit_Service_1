using System.ComponentModel.DataAnnotations;
using AuditService.Common.Enums;

namespace AuditService.Common.Models.Domain;

/// <summary>
///     Audit log transaction
/// </summary>
public class AuditLogTransactionDomainModel : BaseAuditLogModel
{
    /// <summary>
    ///     Service ID
    /// </summary>
    [Required]
    public ServiceStructure Service { get; set; }

}