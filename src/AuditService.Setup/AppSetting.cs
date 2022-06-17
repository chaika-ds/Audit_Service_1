using AuditService.Kafka.Settings;
using AuditService.Setup.Interfaces;
using Microsoft.Extensions.Configuration;
using Tolar.Authenticate.Impl;

namespace AuditService.Setup;

/// <summary>
///     Application settings
/// </summary>
public class AppSetting :
    IKafkaSettings,
    IHealthSettings,
    IJsonData,
    IAuthenticateServiceSettings,
    IPermissionPusher,
    IElasticIndex
{
    /// <summary>
    ///     Application settings
    /// </summary>
    public AppSetting(IConfiguration config)
    {
        ApplySsoSection(config);
        ApplyJsonDataSection(config);
        ApplyKafkaSettings(config);
        ApplyHealthSection(config);
        ApplyPermissionsSection(config);
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
        CriticalErrorsCount = int.Parse(config["KAFKA:HEALTH_CHECK:CRITICAL_ERRORS_COUNT"]);
        ForPeriodInSec = int.Parse(config["KAFKA:HEALTH_CHECK:FOR_PERIOD_IN_SEC"]);
    }

    #endregion

    #region KafkaSettings

    public string? GroupId { get; set; }
    public string? Address { get; set; }
    public string? Topic { get; set; }

    public Dictionary<string, string>? Config { get; set; }

    private void ApplyKafkaSettings(IConfiguration configuration)
    {
        //Address = configuration["KAFKA:CONFIGS:KAFKA_BROKER"];
        //Topic = configuration["KAFKA:KAFKA_TOPICS:KAFKA_TOPIC_AUDITLOG"];
        // todo это прям лютый костыляка, надо это ЧИНИТЬ
        var excludeConfigs = new List<string> { "KAFKA_USERNAME", "KAFKA_PASSWORD", "KAFKA_PREFIX" };
        Config = configuration.GetSection("KAFKA:CONFIGS").GetChildren().Where(w=> !excludeConfigs.Contains(w.Key) ).ToDictionary(x => MapperKafkaKey(x.Key), v => v.Value);
    }
    private string MapperKafkaKey(string key)
    {
        switch (key)
        {
            case "KAFKA_BROKER": return "bootstrap.servers";
            case "KAFKA_CONSUMER_GROUP": return "group.id";
        }

        return key;
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
        Connection = config["SSO:SSO_SERVICE_URL"];
        ServiceId = Guid.Parse(config["SSO:SSO_AUTH_SERVICE_ID"] ?? throw new InvalidOperationException("Wrong ServiceId."));
        ApiKey = config["SSO:SSO_AUTH_API_KEY"];
        RootNodeId = Guid.Parse(config["SSO:SSO_AUTH_ROOT_NODE_ID"] ?? throw new InvalidOperationException("Wrong RootNodeId."));
    }

    #endregion

    #region JSON Data

    /// <summary>
    ///     Categories of service
    /// </summary>
    public string? ServiceCategories { get; set; }

    /// <summary>
    ///     Apply JSON Data configs
    /// </summary>
    private void ApplyJsonDataSection(IConfiguration configuration)
    {
        ServiceCategories = configuration["JSON_DATA:SERVICE_CATEGORIES_PATH"];
    }

    #endregion

    #region ElasticSearch Indexes

    /// <summary>
    ///     Apply ELK indexes configs
    /// </summary>
    private void ApplyElasticSearchIndexesSection(IConfiguration config)
    {
        AuditLog = config["ELASTIC_SEARCH:INDEXES:ELK_INDEX_AUDITLOG"];
        ApplicationLog = config["ELASTIC_SEARCH:INDEXES:ELK_INDEX_APPLOG"];
    }

    /// <summary>
    ///     Audit logs from services
    /// </summary>
    public string? AuditLog { get; set; }

    /// <summary>
    ///     Internal application logs
    /// </summary>
    public string? ApplicationLog { get; set; }

    #endregion

    #region PermisionPusher

    public Guid ServiceIdentificator { get; set; }

    public string TopicOfKafka { get; set; }

    public string? ServiceName { get; set; }

    /// <summary>
    ///     Apply Permission configs
    /// </summary>
    private void ApplyPermissionsSection(IConfiguration config)
    {
        ServiceIdentificator = Guid.Parse(config["SSO:SSO_AUTH_SERVICE_ID"] ?? throw new InvalidOperationException("Wrong ServiceId.")); ;
        TopicOfKafka = config["KAFKA:PermissionsTopic"];
        ServiceName = config["SSO:SSO_SERVICE_NAME"];
    }

    #endregion
}