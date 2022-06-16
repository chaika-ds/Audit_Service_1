namespace AuditService.Common.Exceptions;

public class KafkaConsumerException : Exception
{
    public KafkaConsumerException(string message)
        : base(message)
    {
    }
}
