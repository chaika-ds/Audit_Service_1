namespace AuditService.Data.Domain.Dto;

/// <summary>
///     Category data model
/// </summary>
public class CategoryDto
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
    ///     Action
    /// </summary>
    public string ActionName { get; set; }

    /// <summary>
    ///     Comment
    /// </summary>
    public string Comment { get; set; }
}