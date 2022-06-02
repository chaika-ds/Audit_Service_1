namespace AuditService.Common.Kafka
{
    public class KafkaProducerException : Exception
    {
        public KafkaProducerException(string message)
            : base(message)
        {
        }
    }
}
