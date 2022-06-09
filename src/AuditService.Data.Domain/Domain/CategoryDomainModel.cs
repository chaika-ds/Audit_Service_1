namespace AuditService.Data.Domain.Domain;

/// <summary>
///     Category data model
/// </summary>
public class CategoryDomainModel
{
    /// <summary>
    ///     Code of category
    /// </summary>
    public string CategoryCode { get; set; }
    /// <summary>
    ///     Name of category
    /// </summary>
    public string CategoryName { get; set; }

    /// <summary>
    ///  ActionDto of reference
    /// </summary>
    public ActionDto[] Action { get; set; }
}

