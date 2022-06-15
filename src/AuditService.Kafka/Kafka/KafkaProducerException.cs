namespace AuditService.Kafka.Kafka
{
    public class KafkaProducerException : Exception
    {
        public KafkaProducerException(string message)
            : base(message)
        {
        }
    }
}
