// See https://aka.ms/new-console-template for more information
using AuditService.Common.KafkaTest;

var consumer = new KafkaConsumerTest();
consumer.KafkaConsumerStart("test-topic");
Console.WriteLine("Hello, World!");
