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
        CriticalErrorsCount = int.Parse(config["Health:CriticalErrorsCount"]);
        ForPeriodInSec = int.Parse(config["Health:ForPeriodInSec"]);
    }

    #endregion

    #region KafkaSettings

    public string? GroupId { get; set; }
    public string? Address { get; set; }
    public string? Topic { get; set; }

    public Dictionary<string, string>? Config { get; set; }

    private void ApplyKafkaSettings(IConfiguration configuration)
    {
        //Address = config["Kafka:Address"];
        //Topic = config["Kafka:AuditlogTopic"];
        Config = configuration.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);
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
    public string? ServiceCategories { get; set; }

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
        ServiceIdentificator = Guid.Parse(config["ServiceId"] ?? throw new InvalidOperationException("Wrong ServiceId.")); ;
        TopicOfKafka = config["Kafka:PermissionsTopic"];
        ServiceName = config["ServiceName"];
    }

    #endregion
}