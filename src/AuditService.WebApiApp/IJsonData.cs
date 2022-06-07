namespace AuditService.WebApiApp;

/// <summary>
///     JSON data configs
/// </summary>
public interface IJsonData
{
    /// <summary>
    ///     Path to file with categories
    /// </summary>
    string ServiceCategories { get; set; }
}