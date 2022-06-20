using AuditService.Setup.ConfigurationSettings;
using Microsoft.Extensions.Configuration;

namespace AuditService.Setup.AppSettings;

internal class ElasticSearchSettings : IElasticSearchSettings
{
    public ElasticSearchSettings(IConfiguration configuration) => ApplySettings(configuration);

    public string? AuditLog { get; set; }

    public string? ApplicationLog { get; set; }

    public string? ConnectionUrl { get; set; }

    /// <summary>
    ///     Apply ELK indexes configs
    /// </summary>
    private void ApplySettings(IConfiguration config)
    {
        AuditLog = config["ELASTIC_SEARCH:INDEXES:ELK_INDEX_AUDITLOG"];
        ApplicationLog = config["ELASTIC_SEARCH:INDEXES:ELK_INDEX_APPLOG"];
        ConnectionUrl = config["ELASTIC_SEARCH:ELK_CONNECTION_URL"];
    }
}