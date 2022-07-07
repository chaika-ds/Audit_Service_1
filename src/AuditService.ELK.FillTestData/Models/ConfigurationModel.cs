using AuditService.Common.Enums;

namespace AuditService.ELK.FillTestData.Models;

/// <summary>
///     Model of fillers
/// </summary>
internal class ConfigurationModel
{
    /// <summary>
    ///     Service ID
    /// </summary>
    public ServiceStructure? ServiceName { get; set; }

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

    /// <summary>
    ///     Count rows for generation
    /// </summary>
    public int Count { get; set; }
}