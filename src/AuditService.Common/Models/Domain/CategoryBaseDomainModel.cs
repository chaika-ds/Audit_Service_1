namespace AuditService.Common.Models.Domain;

public class CategoryBaseDomainModel
{
    public CategoryBaseDomainModel()
    {
        CategoryCode = string.Empty;
        CategoryName = string.Empty;
    }

    /// <summary>
    ///     Code of category
    /// </summary>
    public string CategoryCode { get; set; }

    /// <summary>
    ///     Name of category
    /// </summary>
    public string CategoryName { get; set; }
}