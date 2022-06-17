using AuditService.Common.Enums;

namespace AuditService.Common.Models.Domain;

/// <summary>
///     Action data model
/// </summary>
public class ActionDomainModel
{
    public ActionDomainModel()
    {
        Description = string.Empty;
    }

    /// <summary>
    ///     Name
    /// </summary>
    public ActionType Name { get; set; }

    /// <summary>
    ///   Description
    /// </summary>
    public string Description { get; set; }
}