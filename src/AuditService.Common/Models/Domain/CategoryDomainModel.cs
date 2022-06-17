namespace AuditService.Common.Models.Domain;

/// <summary>
///     Category data model
/// </summary>
public class CategoryDomainModel : CategoryBaseDomainModel
{
    public CategoryDomainModel()
    {
        Action = new List<ActionDomainModel>().ToArray();
    }

    /// <summary>
    ///     ActionDto of reference
    /// </summary>
    public ActionDomainModel[] Action { get; set; }
}