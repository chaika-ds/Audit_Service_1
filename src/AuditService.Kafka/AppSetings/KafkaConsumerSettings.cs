using Microsoft.Extensions.Configuration;
using Tolar.Kafka;
using Tolar.Web.Tools;

namespace AuditService.Kafka.AppSetings;

internal class KafkaConsumerSettings : IKafkaConsumerSettings
{
    public KafkaConsumerSettings(IConfiguration configuration)
    {
        configuration.GetSection("Kafka").Bind(this);

        Config = SettingsHelper.GetKafkaConfiguration(configuration);
    }

    public int MaxTimeoutMsec { get; set; }

    public int MaxThreadsCount { get; set; }

    public Dictionary<string, string>? Config { get; set; }
}