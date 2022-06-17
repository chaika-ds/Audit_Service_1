namespace AuditService.Common.Exceptions;

public class KafkaConsumerException : Exception
{
    /// <summary>
    ///     Kafka cunsumer exception
    /// </summary>
    /// <param name="message">Message</param>
    public KafkaConsumerException(string message) : base(message)
    {
    }
}