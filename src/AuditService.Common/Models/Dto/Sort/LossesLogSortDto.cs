using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Sort;

/// <summary>
///     Losses l og sorting model
/// </summary>
public class LossesLogSortDto : ISort
{
    /// <summary>
    ///     Sort type for losses log
    ///     (Enumeration of fields for selection)
    /// </summary>
    public LossesLogSortType FieldSortType { get; set; }

    /// <summary>
    ///     Sortable type
    /// </summary>
    public SortableType SortableType { get; set; }
}