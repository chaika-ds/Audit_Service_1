using AuditService.Data.Domain.Enums;

namespace AuditService.Data.Domain.Dto.Filter;

/// <summary>
///     Audit log filter. Sort model
/// </summary>
public class AuditLogSortDto
{
    /// <summary>
    ///     Number of log record
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    ///     Date and time of the event
    /// </summary>
    public SortableType SortableType { get; set; }
}