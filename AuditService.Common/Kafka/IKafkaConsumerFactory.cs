namespace AuditService.Common.Kafka
{
    public interface IKafkaConsumerFactory
    {
        public IKafkaConsumer CreateConsumer(string topic);
    }
}
