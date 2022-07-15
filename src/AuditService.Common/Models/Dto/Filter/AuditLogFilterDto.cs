using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Filter;

/// <summary>
///     Audit log filter. Filter model
/// </summary>
public class AuditLogFilterDto
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
    public Guid? EntityId { get; set; }

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
    ///     Start Date
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    ///     End Date
    /// </summary>
    public DateTime? EndDate { get; set; }
}