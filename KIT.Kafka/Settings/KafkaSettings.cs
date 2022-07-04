using Microsoft.Extensions.Configuration;
using Tolar.Kafka;
using Tolar.Web.Tools;

namespace KIT.Kafka.Settings;

/// <summary>
/// Kafka settings
/// </summary>
internal class KafkaSettings : IKafkaSettings
{
    public KafkaSettings(IConfiguration configuration)
    {
        Config = SettingsHelper.GetKafkaConfiguration(configuration);
    }

    /// <summary>
    /// No longer in use
    /// </summary>
    public string? Topic => null;

    /// <summary>
    /// Collection of keys and values to configure Kafka
    /// </summary>
    public Dictionary<string, string>? Config { get; }
}