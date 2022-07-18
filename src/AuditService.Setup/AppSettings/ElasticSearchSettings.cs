using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

/// <summary>
///     Elastic service settings model
/// </summary>
internal class ElasticSearchSettings : IElasticSearchSettings
{
    public ElasticSearchSettings(IConfiguration configuration) => ApplySettings(configuration);

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
    ///     Connection URL for ELK
    /// </summary>
    public string? ConnectionUrl { get; set; }

    /// <summary>
    ///     Apply ELK indexes configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        AuditLog = config["ElasticSearch:Indexes:AuditLog"];
        ApplicationLog = config["ElasticSearch:Indexes:ApplicationLog"];
        PlayerChangesLog = config["ElasticSearch:Indexes:PlayerChangesLog"];
        BlockedPlayersLog = config["ElasticSearch:Indexes:BlockedPlayersLog"];
        ConnectionUrl = config["ElasticSearch:ConnectionString"];
    }
}