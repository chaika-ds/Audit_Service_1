namespace AuditService.Common.Models.Domain;

/// <summary>
///     Category data model
/// </summary>
public class CategoryDomainModel
{
    public CategoryDomainModel()
    {
        CategoryCode = string.Empty;
        CategoryName = string.Empty;
        Action = new List<ActionDomainModel>().ToArray();
    }

    /// <summary>
    ///     Code of category
    /// </summary>
    public string CategoryCode { get; set; }

    /// <summary>
    ///     Name of category
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    ///     ActionDto of reference
    /// </summary>
    public ActionDomainModel[] Action { get; set; }
}