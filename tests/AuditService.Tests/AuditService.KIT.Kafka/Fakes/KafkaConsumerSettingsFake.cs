using Microsoft.Extensions.Configuration;
using Tolar.Kafka;
using Tolar.Web.Tools;

namespace AuditService.Tests.AuditService.KIT.Kafka.Fakes;

/// <summary>
/// Fake Kafka consumer settings
/// </summary>
public class KafkaConsumerSettingsFake : IKafkaConsumerSettings
{
    public KafkaConsumerSettingsFake(IConfiguration configuration)
    {
        Config = SettingsHelper.GetKafkaConfiguration(configuration);
        MaxTimeoutMsec = 5000;
        MaxThreadsCount = 5;
    }

    /// <summary>
    /// Maximum timeout in milliseconds
    /// </summary>
    public int MaxTimeoutMsec { get; set; }

    /// <summary>
    /// Maximum number of threads to consume messages
    /// </summary>
    public int MaxThreadsCount { get; set; }

    /// <summary>
    /// Collection of keys and values to configure Kafka
    /// </summary>
    public Dictionary<string, string>? Config { get; set; }
}