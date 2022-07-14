using AuditService.Common.Enums;

namespace AuditService.Common.Models.Dto.Sort;

/// <summary>
///     Log filter. Sort model
/// </summary>
public class LogSortDto : ISort
{
    /// <summary>
    ///     Date and time of the event
    /// </summary>
    public SortableType SortableType { get; set; }
}