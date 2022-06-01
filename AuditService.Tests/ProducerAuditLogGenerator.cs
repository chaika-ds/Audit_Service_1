using AuditService.Common.KafkaTest;
using AuditService.IntegrationTests;
using System.Threading.Tasks;
using Xunit;

namespace AuditService.Tests
{
    public class ProducerAuditLogGenerator
    {
        private readonly string _topicName = "test-topic6";

        
        public async Task KafkaProducer_AuditLog_Generator123()
        {
            var builder = new AuditLogMessageDtoBuilder();

            var dto = builder.Get();
            var topik = _topicName;
            var auditLog = "121";//Helper.SerializeToString(dto);
            var producerTest = new KafkaProducerTest();
            await producerTest.KafkaProducerStart(topik, auditLog);

            var consumer = new KafkaConsumerTest();
            consumer.KafkaConsumerStart(_topicName);

        }


   
        public async Task KafkaConsumer_AuditLog()
        {
            var consumer = new KafkaConsumerTest();
            consumer.KafkaConsumerStart(_topicName);
        }
    }
}