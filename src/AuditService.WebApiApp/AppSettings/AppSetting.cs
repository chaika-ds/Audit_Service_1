using AuditService.Kafka.Settings;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Tolar.Authenticate.Impl;
using Tolar.Redis;

namespace AuditService.WebApiApp.AppSettings;

/// <summary>
///     Application settings
/// </summary>
public class AppSetting :
    Kafka.Settings.IKafkaSettings,
    IHealthSettings,
    IJsonData,
    IAuthenticateServiceSettings,
    IPermissionPusherSettings,
    IElasticIndex,
    IRedisSettings
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
        _permissionPusherTopic = config["Kafka:PermissionsTopic"];
        ApplyPermissionsSection(config);
        ApplyRedisSection(config);
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
    public string GroupId { get; set; }
    public string Address { get; set; }
    public string Topic { get; set; }

    public Dictionary<string, string> Config { get; set; }

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

    #region PermisionPusher
    Guid IPermissionPusherSettings.ServiceId { get => ServiceId; set { throw new NotSupportedException(); } }

    public string ServiceName { get; set; }

    private readonly string _permissionPusherTopic;

    string IPermissionPusherSettings.Topic => _permissionPusherTopic;

    /// <summary>
    ///     Apply Permission configs
    /// </summary>
    private void ApplyPermissionsSection(IConfiguration config)
    {
        ServiceName = config["ServiceName"];
    }

    #endregion

    #region Redis
    /// <summary>
    ///     Apply Redis configs
    /// </summary>
    public string RedisConnectionString { get; private set; }
    public string RedisPrefix { get; private set; }
    
    private void ApplyRedisSection(IConfiguration config)
    {
        RedisConnectionString = config["RedisCache:ConnectionString"];
        RedisPrefix = config["RedisCache:InstanceName"] ?? "RedisCache";
    }
    #endregion
}
