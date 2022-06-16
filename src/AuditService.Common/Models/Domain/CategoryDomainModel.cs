namespace AuditService.Common.Models.Domain;

/// <summary>
///     Category data model
/// </summary>
public class CategoryDomainModel : CategoryBaseDomainModel
{
    /// <summary>
    ///  ActionDto of reference
    /// </summary>
    public ActionDomainModel[] Action { get; set; }
}