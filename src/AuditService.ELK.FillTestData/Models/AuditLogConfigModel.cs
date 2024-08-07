using AuditService.Common.Enums;

namespace AuditService.ELK.FillTestData.Models;

/// <summary>
///     Model of app settings
/// </summary>
public class AuditLogConfigModel: BaseConfig
{
    /// <summary>
    ///     Service ID
    /// </summary>
    public ModuleName? ServiceName { get; set; }

    /// <summary>
    ///     Action type
    /// </summary>
    public ActionType? ActionName { get; set; }

    /// <summary>
    ///     Category of action
    /// </summary>
    public string? CategoryCode { get; set; }

    /// <summary>
    ///     Node type
    /// </summary>
    public NodeType? NodeType { get; set; }
}