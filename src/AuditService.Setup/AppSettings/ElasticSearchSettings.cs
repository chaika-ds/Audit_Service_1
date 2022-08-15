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
    ///     Player card changelog
    /// </summary>
    public string? PlayerChangesLog { get; set; }

    /// <summary>
    ///     Log of blocked players
    /// </summary>
    public string? BlockedPlayersLog { get; set; }

    /// <summary>
    ///     Visit log
    /// </summary>
    public string? VisitLog { get; set; }

    /// <summary>
    ///     Losses log
    /// </summary>
    public string? LossesLog { get; set; }

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

    /// <summary>
    ///     Apply ELK indexes configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        AuditLog = config["ElasticSearch:Indexes:AuditLog"];
        PlayerChangesLog = config["ElasticSearch:Indexes:PlayerChangesLog"];
        BlockedPlayersLog = config["ElasticSearch:Indexes:BlockedPlayersLog"];
        VisitLog = config["ElasticSearch:Indexes:VisitLog"];
        LossesLog = config["ElasticSearch:Indexes:LossesLog"];
        ConnectionUrl = config["ElasticSearch:ConnectionString"];
        UserName = config["ElasticSearch:UserName"];
        Password = config["ElasticSearch:Password"];
    }
}