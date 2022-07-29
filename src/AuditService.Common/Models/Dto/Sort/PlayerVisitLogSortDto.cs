using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Sort;

/// <summary>
///     Player visit log sorting model
/// </summary>
public class PlayerVisitLogSortDto : ISort
{
    /// <summary>
    ///     Sort type for player visit log
    ///     (Enumeration of fields for selection)
    /// </summary>
    public PlayerVisitLogSortType FieldSortType { get; set; }

    /// <summary>
    ///     Sortable type
    /// </summary>
    public SortableType SortableType { get; set; }
}