// See https://aka.ms/new-console-template for more information
using AuditService.Common.KafkaTest;


var cc = new KafkaProducerTest();
await cc.KafkaProducerStart();

//var cc1 = new KafkaConsumerTest();
//cc1.KafkaConsumerStart();

Console.WriteLine("Hello, World!");


