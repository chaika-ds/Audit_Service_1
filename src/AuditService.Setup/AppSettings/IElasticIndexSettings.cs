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

    /// <summary>
    /// Player card changelog
    /// </summary>
    public string? PlayerChangesLog { get; set; }

    /// <summary>
    /// Log of blocked players
    /// </summary>
    public string? BlockedPlayersLog { get; set; }

    /// <summary>
    /// Visit log
    /// </summary>
    public string? VisitLog { get; set; }
}