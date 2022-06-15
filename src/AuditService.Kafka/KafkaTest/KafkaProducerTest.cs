using Confluent.Kafka;

namespace AuditService.Kafka.KafkaTest
{
    public class KafkaProducerTest
    {
        public async Task KafkaProducerStartAsync(string topicTest, string serializedObj)
        {
            var config = new ProducerConfig { 
                BootstrapServers = "localhost:9092",
                //SaslMechanism = SaslMechanism.Plain,
                //SecurityProtocol = SecurityProtocol.SaslPlaintext,
                //SaslUsername = "1",
                //SaslPassword = "1",
            };

            // If serializers are not specified, default serializers from
            // `Confluent.Kafka.Serializers` will be automatically used where
            // available. Note: by default strings are encoded as UTF8.
            using (var p = new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    var dr = await p.ProduceAsync(topicTest, new Message<Null, string> { Value = serializedObj });
                    Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
                }
                catch (ProduceException<Null, string> e)
                {
                    Console.WriteLine($"Delivery failed: {e.Error.Reason}");
                }
            }
        }
    }
}
