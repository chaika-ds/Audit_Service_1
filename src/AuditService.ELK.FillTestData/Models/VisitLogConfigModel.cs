using AuditService.Common.Enums;

namespace AuditService.ELK.FillTestData.Models;

/// <summary>
///     Visit log config model
/// </summary>
internal class VisitLogConfigModel : BaseConfig
{
    /// <summary>
    ///     Type of the log of visits
    /// </summary>
    public VisitLogType Type { get; set; }
}