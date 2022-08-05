using AuditService.Setup.AppSettings;
using AuditService.Tests.Resources;

namespace AuditService.Tests.Fakes.Setup.ELK;

/// <summary>
///     Fake elastic search settings
/// </summary>
internal class ElasticSearchSettingsFake : IElasticSearchSettings
{
    /// <summary>
    ///     Audit logs from services
    /// </summary>
    public string? AuditLog { get; set; }

    /// <summary>
    /// Player card changelog
    /// </summary>
    public string? PlayerChangesLog { get; set; }

    /// <summary>
    /// Log of blocked players
    /// </summary>
    public string? BlockedPlayersLog { get; set; }

    /// <summary>
    ///     Visit log
    /// </summary>
    public string? VisitLog { get; set; }

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
    public ElasticSearchSettingsFake()
    {
        VisitLog = TestResources.VisitLog;
        AuditLog = TestResources.DefaultIndex;
        PlayerChangesLog = TestResources.PlayerChangesLog;
        BlockedPlayersLog = TestResources.BlockedPlayersLog;
        ConnectionUrl = TestResources.ConnectionUrl;
    }
}
