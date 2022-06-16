using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Filter;

/// <summary>
///     Audit log filter. Sort model
/// </summary>
public class AuditLogSortDto
{
    public AuditLogSortDto()
    {
        ColumnName = string.Empty;
    }

    /// <summary>
    ///     Number of log record
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    ///     Date and time of the event
    /// </summary>
    public SortableType SortableType { get; set; }
}