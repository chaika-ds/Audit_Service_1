namespace AuditService.Kafka.Services;

/// <summary>
///     Configuration section of kafka topics
/// </summary>
public interface IKafkaTopics
{
    /// <summary>
    ///     Topic of AuditLog
    /// </summary>
    string AuditLog { get; set; }
}