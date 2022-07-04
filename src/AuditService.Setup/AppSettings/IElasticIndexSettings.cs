namespace AuditService.Setup.AppSettings;

/// <summary>
///     ElasticSearch available indexes
/// </summary>
public interface IElasticIndexSettings
{
    /// <summary>
    ///     Audit logs from services
    /// </summary>
    public string? AuditLog { get; set; }

    /// <summary>
    ///     Internal application logs
    /// </summary>
    public string? ApplicationLog { get; set; }
}