using Microsoft.Extensions.Configuration;
using Tolar.Kafka;
using Tolar.Web.Tools;

namespace KIT.Kafka.Settings;

/// <summary>
/// Kafka consumer settings
/// </summary>
public class KafkaConsumerSettings : IKafkaConsumerSettings
{
    public KafkaConsumerSettings(IConfiguration configuration)
    {
        Config = SettingsHelper.GetKafkaConfiguration(configuration);
        MaxTimeoutMsec = Convert.ToInt32(configuration["Kafka:MaxTimeoutMsec"]);
        MaxThreadsCount = Convert.ToInt32(configuration["Kafka:MaxThreadsCount"]);
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