using AuditService.Kafka.Services;
using Microsoft.Extensions.Configuration;

namespace AuditService.Kafka.AppSetings;

/// <summary>
///     Configuration section of kafka topics
/// </summary>
public class KafkaTopics : IKafkaTopics
{
    /// <summary>
    ///     Topic of AuditLog
    /// </summary>
    public string AuditLog { get; set; }

    public KafkaTopics(IConfiguration config)
    {
        AuditLog = config["Kafka:Topics:AuditLog"];
    }
}