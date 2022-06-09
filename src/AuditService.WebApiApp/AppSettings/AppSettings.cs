using AuditService.Common.Health;
using AuditService.Common.Kafka;
using Microsoft.Extensions.Configuration;
using Tolar.Authenticate.Impl;

namespace AuditService.WebApiApp.AppSettings;

/// <summary>
///     Application settings
/// </summary>
public class AppSettings : IKafkaConsumerSettings, IHealthSettings, IJsonData, IAuthenticateServiceSettings, IElasticIndex
{
    /// <summary>
    ///     Application settings
    /// </summary>
    public AppSettings(IConfiguration config)
    {
        ApplySsoSection(config);
        ApplyJsonDataSection(config);
        ApplyKafkaSection(config);
        ApplyHealthSection(config);
        ApplyElasticSearchIndexesSection(config);
    }
    
    #region Health

    public int CriticalErrorsCount { get; set; }
    public int ForPeriodInSec { get; set; }

    /// <summary>
    ///     Apply Health configs
    /// </summary>
    private void ApplyHealthSection(IConfiguration config)
    {
        CriticalErrorsCount = int.Parse(config["Health:CriticalErrorsCount"]);
        ForPeriodInSec = int.Parse(config["Health:ForPeriodInSec"]);
    }

    #endregion

    #region Kafka

    public int MaxTimeoutMsec { get; set; }
    public int MaxThreadsCount { get; set; }

    public Dictionary<string, string> Config { get; set; }

    /// <summary>
    ///     Apply Kafka configs
    /// </summary>
    private void ApplyKafkaSection(IConfiguration config)
    {
        MaxTimeoutMsec = int.Parse(config["Kafka:MaxTimeoutMsec"]);
        MaxThreadsCount = int.Parse(config["Kafka:MaxThreadsCount"]);

        Config = config.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);

        ApplyKafkaAliases(config, Config);
    }

    private void ApplyKafkaAliases(IConfiguration configuration, Dictionary<string, string> config)
    {
        var aliases = configuration.GetSection("Kafka:Aliases").GetChildren().ToDictionary(x => x.Key, v => v.Value);

        foreach (var item in aliases)
        {
            var value = configuration[$"Kafka:{item.Key}"];

            if (!string.IsNullOrEmpty(value)) config[item.Value] = value;
        }
    }

    #endregion

    #region SSO

    /// <summary>
    ///     Link to SSO
    /// </summary>
    public string? Connection { get; private set; }

    /// <summary>
    ///     Service ID
    /// </summary>
    public Guid ServiceId { get; private set; }

    /// <summary>
    ///     API Secret Key
    /// </summary>
    public string? ApiKey { get; private set; }

    /// <summary>
    ///     Root id from structure of casino
    /// </summary>
    public Guid RootNodeId { get; private set; }

    /// <summary>
    ///     Apply SSO configs
    /// </summary>
    private void ApplySsoSection(IConfiguration config)
    {
        Connection = config["AuthConnection"];
        ServiceId = Guid.Parse(config["ServiceId"] ?? throw new InvalidOperationException("Wrong ServiceId."));
        ApiKey = config["ApiKey"];
        RootNodeId = Guid.Parse(config["RootNodeId"] ?? throw new InvalidOperationException("Wrong RootNodeId."));
    }

    #endregion

    #region JSON Data

    /// <summary>
    ///     Categories of service
    /// </summary>
    public string ServiceCategories { get; set; }

    /// <summary>
    ///     Apply JSON Data configs
    /// </summary>
    private void ApplyJsonDataSection(IConfiguration configuration)
    {
        ServiceCategories = configuration["JsonData:ServiceCategories"];
    }

    #endregion

    #region ElasticSearch Indexes

    /// <summary>
    ///     Apply ELK indexes configs
    /// </summary>
    private void ApplyElasticSearchIndexesSection(IConfiguration config)
    {
        AuditLog = config["ElasticSearch:Indexes:AuditLog"];
        ApplicationLog = config["ElasticSearch:Indexes:ApplicationLog"];
    }

    /// <summary>
    ///     Audit logs from services
    /// </summary>
    public string AuditLog { get; set; }

    /// <summary>
    ///     Internal application logs
    /// </summary>
    public string ApplicationLog { get; set; }

    #endregion
}