namespace KIT.Kafka.Settings.Interfaces;

/// <summary>
/// Configuration section of kafka topics
/// </summary>
public interface IKafkaTopics
{
    /// <summary>
    /// Topic of AuditLog
    /// </summary>
    string AuditLog { get; set; }

    /// <summary>
    /// Topic of Permissions
    /// </summary>
    string Permissions { get; set; }
}