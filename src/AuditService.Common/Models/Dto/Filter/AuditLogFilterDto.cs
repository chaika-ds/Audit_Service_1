using System.ComponentModel.DataAnnotations;
using AuditService.Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.Common.Models.Dto.Filter;

/// <summary>
///     Audit log filter. Filter model
/// </summary>
public class AuditLogFilterDto : ILogFilter
{
    /// <summary>
    ///     Audit log filter. Filter model
    /// </summary>
    public AuditLogFilterDto()
    {
        Action = new List<ActionType>();
    }

    /// <summary>
    ///     Service ID
    /// </summary>
    public ModuleName? Service { get; set; }

    /// <summary>
    ///     ID of the node where the change occurred
    /// </summary>
    public Guid? NodeId { get; set; }

    /// <summary>
    ///     Category of actions (depending on modules)
    /// </summary>
    public string? CategoryCode { get; set; }

    /// <summary>
    ///     ID of the logged entity (possible types of UUID/Long values)
    /// </summary>
    public string? EntityId { get; set; }

    /// <summary>
    ///     Types of action
    /// </summary>
    public IEnumerable<ActionType> Action { get; set; }

    /// <summary>
    ///     IP-adress
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    ///     User login
    /// </summary>
    public string? Login { get; set; }

    /// <summary>
    ///      Start date
    /// </summary>
    [Required]
    [ModelBinder(Name = "startDate")]
    public DateTime TimestampFrom { get; set; }

    /// <summary>
    ///     End date
    /// </summary>
    [Required]
    [ModelBinder(Name = "endDate")]
    public DateTime TimestampTo { get; set; }
}