using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Sort;

/// <summary>
///     Sorting Model
/// </summary>
public interface ISort
{
    /// <summary>
    ///     Sortable type
    /// </summary>
    SortableType SortableType { get; set; }
}