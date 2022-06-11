using AuditService.Data.Domain.Enums;

namespace AuditService.Data.Domain.Domain;

/// <summary>
///     Action data model
/// </summary>
public class ActionDomainModel
{
    /// <summary>
    ///     Name
    /// </summary>
    public ActionType Name { get; set; }

    /// <summary>
    ///   Description
    /// </summary>
    public string Description { get; set; }
}