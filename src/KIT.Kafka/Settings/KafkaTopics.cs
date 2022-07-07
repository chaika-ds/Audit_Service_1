using KIT.Kafka.Settings.Interfaces;
using Microsoft.Extensions.Configuration;

namespace KIT.Kafka.Settings;

/// <summary>
/// Configuration section of kafka topics
/// </summary>
public class KafkaTopics : IKafkaTopics
{
    /// <summary>
    /// Topic of AuditLog
    /// </summary>
    public string AuditLog { get; set; }

    /// <summary>
    /// Topic of Permissions
    /// </summary>
    public string Permissions { get; set; }

    public KafkaTopics(IConfiguration config)
    {
        AuditLog = config["Kafka:Topics:AuditLog"];
        Permissions = config["Kafka:Topics:PermissionsTopic"];
    }
}