namespace AuditService.Setup.AppSettings;

/// <summary>
///     Configuration section of ELK
/// </summary>
public interface IElasticSearchSettings : IElasticIndexSettings
{
    /// <summary>
    ///     Connection URL for ELK
    /// </summary>
    public string? ConnectionUrl { get; set; }

    /// <summary>
    ///     UserName for ELK
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    ///     Password for ELK
    /// </summary>
    public string? Password { get; set; }
}