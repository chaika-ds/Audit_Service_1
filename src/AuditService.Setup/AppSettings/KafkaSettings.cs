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
        Config = configuration.GetSection("KAFKA:CONFIGS").GetChildren().Where(w => !excludeConfigs.Contains(w.Key)).ToDictionary(x => MapperKafkaKey(x.Key), v => v.Value);
        Topic = configuration["KAFKA:KAFKA_TOPICS:KAFKA_TOPIC_AUDITLOG"];
    }

    /// <summary>
    ///     Convert setting keys
    /// </summary>
    private string MapperKafkaKey(string key)
    {
        return key switch
        {
            "KAFKA_BROKER" => "bootstrap.servers",
            "KAFKA_CONSUMER_GROUP" => "group.id",
            _ => key
        };
    }
}