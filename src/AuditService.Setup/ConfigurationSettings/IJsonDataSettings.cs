namespace AuditService.Setup.ConfigurationSettings;

/// <summary>
///     JSON data configs
/// </summary>
public interface IJsonDataSettings
{
    /// <summary>
    ///     Path to file with categories
    /// </summary>
    string? ServiceCategories { get; set; }
}