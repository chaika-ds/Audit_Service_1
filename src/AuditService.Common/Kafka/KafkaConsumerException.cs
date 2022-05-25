namespace AuditService.Common.Kafka
{
    public class KafkaConsumerException : Exception
    {
        public KafkaConsumerException(string message)
            : base(message) 
        {
        }
    }
}
