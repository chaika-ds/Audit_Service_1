﻿using Confluent.Kafka;

namespace AuditService.Kafka.KafkaTest
{
    public class KafkaConsumerTest
    {
        public void KafkaConsumerStart(string topicName)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "AuditService-uat",
                BootstrapServers = "localhost:9092",
                // Note: The AutoOffsetReset property determines the start offset in the event
                // there are not yet any committed offsets for the consumer group for the
                // topic/partitions of interest. By default, offsets are committed
                // automatically, so in this example, consumption will only start from the
                // earliest message in the topic 'my-topic' the first time you run the program.
                AutoOffsetReset = AutoOffsetReset.Latest,
                //EnablePartitionEof = true
            };

            using (var c = new ConsumerBuilder<Ignore, string>(conf)
                .SetValueDeserializer(Deserializers.Utf8)
                .Build())
            {
                c.Subscribe(topicName);

                var cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };

                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    // Ensure the consumer leaves the group cleanly and final offsets are committed.
                    c.Close();
                }
            }
        }
    }
}