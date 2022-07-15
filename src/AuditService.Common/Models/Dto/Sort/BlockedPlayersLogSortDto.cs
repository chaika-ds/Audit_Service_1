using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Sort;

/// <summary>
///     Blocked players log sorting model
/// </summary>
public class BlockedPlayersLogSortDto : ISort
{
    /// <summary>
    ///     Sort type for blocked player log
    ///     (Enumeration of fields for selection)
    /// </summary>
    public BlockedPlayersLogSortType FieldSortType { get; set; }

    /// <summary>
    ///     Sortable type
    /// </summary>
    public SortableType SortableType { get; set; }
}