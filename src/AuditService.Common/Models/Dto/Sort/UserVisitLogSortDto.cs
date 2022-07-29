using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Sort;

/// <summary>
///     User visit log sorting model
/// </summary>
public class UserVisitLogSortDto : ISort
{
    /// <summary>
    ///     Sort type for user visit log
    ///     (Enumeration of fields for selection)
    /// </summary>
    public UserVisitLogSortType FieldSortType { get; set; }

    /// <summary>
    ///     Sortable type
    /// </summary>
    public SortableType SortableType { get; set; }
}