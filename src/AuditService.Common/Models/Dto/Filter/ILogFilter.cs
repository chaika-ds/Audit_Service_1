namespace AuditService.Common.Models.Dto.Filter;

/// <summary>
///     Log filter
/// </summary>
public interface ILogFilter
{
    /// <summary>
    ///     Timestamp from
    /// </summary>
    DateTime TimestampFrom { get; set; }

    /// <summary>
    ///     Timestamp to
    /// </summary>
    DateTime TimestampTo { get; set; }
}