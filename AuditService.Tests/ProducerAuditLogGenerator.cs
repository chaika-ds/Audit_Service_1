using AuditService.Common;
using AuditService.Common.KafkaTest;
using AuditService.IntegrationTests;
using System.Threading.Tasks;
using Xunit;

namespace AuditService.Tests
{
    public class ProducerAuditLogGenerator
    {
        [Fact]
        public async Task KafkaProducer_AuditLog_Generator()
        {
            var builder = new AuditLogMessageDtoBuilder();

            for (int i = 0; i < 10; i++)
            {
                var dto = builder.Get();
                var topik = "uat.auditlog.messages";
                var auditLog = Helper.SerializeToString(dto);
                var producerTest = new KafkaProducerTest();
                await producerTest.KafkaProducerStart(topik, auditLog);
            }
        }
    }    
}