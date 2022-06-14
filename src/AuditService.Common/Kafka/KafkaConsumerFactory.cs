using bgTeam.Extensions;
using Microsoft.Extensions.Logging;
using Tolar.Kafka;

namespace AuditService.Kafka.Kafka
{
    /// <summary>
    /// Factory for creating Kafka consumers
    /// </summary>
    public class KafkaConsumerFactory : IKafkaConsumerFactory
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly IKafkaConsumerSettings _kafkaSettings;

        public KafkaConsumerFactory(ILoggerFactory loggerFactory, IKafkaConsumerSettings kafkaSettings)
        {
            _loggerFactory = loggerFactory;
            _kafkaSettings = kafkaSettings;
        }

        public IKafkaConsumer CreateConsumer(string topic)
        {
            topic.CheckNull(nameof(topic));
            return new KafkaConsumer(_loggerFactory.CreateLogger<KafkaConsumer>(), _kafkaSettings, topic);
        }

        Tolar.Kafka.IKafkaConsumer IKafkaConsumerFactory.CreateConsumer(string topic)
        {
            throw new NotImplementedException();
        }
    }
}
