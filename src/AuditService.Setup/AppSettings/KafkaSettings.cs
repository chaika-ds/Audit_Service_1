using Microsoft.Extensions.Configuration;
using Tolar.Kafka;

namespace AuditService.Setup.AppSettings;

internal class KafkaSettings : IKafkaSettings
{
    public KafkaSettings(IConfiguration configuration) => ApplySettings(configuration);

    public string? Topic { get; private set; }

    public Dictionary<string, string>? Config { get; private set; }

    /// <summary>
    ///     Apply Kafka configs
    /// </summary>
    private void ApplySettings(IConfiguration configuration)
    {
        // todo @d.chaika надо тебе порефакторить это. не нравится мне все эти выбороки. надо сделать более аккуратно
        
        var excludeConfigs = new List<string> { "KAFKA_USERNAME", "KAFKA_PASSWORD", "KAFKA_PREFIX" };
        Config = configuration.GetSection("Kafka:ProducerConfig").GetChildren().Where(w => !excludeConfigs.Contains(w.Key)).ToDictionary(x => x.Key, v => v.Value);
        Topic = configuration["Kafka:Topics:AuditLog"];
    }
}