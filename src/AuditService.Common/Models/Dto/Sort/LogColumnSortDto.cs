using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Sort;

/// <summary>
///     Sorting model with the ability
///     to specify a field for sorting
/// </summary>
public class LogColumnSortDto : ISort
{
    public LogColumnSortDto()
    {
        ColumnName = string.Empty;
    }

    /// <summary>
    ///     The name of the field to sort by
    /// </summary>
    public string ColumnName { get; set; }

    /// <summary>
    ///     Sortable type
    /// </summary>
    public SortableType SortableType { get; set; }
}