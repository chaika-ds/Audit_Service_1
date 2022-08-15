using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.LossesLog;

/// <summary>
///     Losses log
/// </summary>
public class LossesLogDomainModel : LossesLogBaseModel
{
    public LossesLogDomainModel()
    {
        Login = string.Empty;
        CurrencyCode = string.Empty;
    }

    /// <summary>
    ///     Date and time of the event (ISO 8601 UTC)
    /// </summary>
    [Required]
    public DateTime CreateDate { get; set; }
}