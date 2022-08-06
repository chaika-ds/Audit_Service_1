namespace AuditService.Tests.Fakes.Kafka.Messages;

/// <summary>
/// Base  consumer message fake
/// </summary>
public class BaseConsumerMessageFake
{
    /// <summary>
    ///    Message
    /// </summary>
    public string? Message { get; set; }
    
    /// <summary>
    ///    Timestamp
    /// </summary>
    public DateTime Timestamp { get; set; }
    
    /// <summary>
    ///    EventType
    /// </summary>
    public string? EventType { get; set; }
}