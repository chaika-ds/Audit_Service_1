using Microsoft.Extensions.Configuration;
using Tolar.Kafka;
using Tolar.Web.Tools;

namespace AuditService.Setup.AppSettings;

internal class KafkaSettings : IKafkaSettings
{
    public KafkaSettings(IConfiguration configuration)
    {
        Config = SettingsHelper.GetKafkaConfiguration(configuration);
        Topic = configuration["Kafka:Topics:AuditLog"];
    }

    public string? Topic { get; }

    public Dictionary<string, string>? Config { get; }
}