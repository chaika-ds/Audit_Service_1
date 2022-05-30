using AuditService.Common;
using AuditService.Common.KafkaTest;
using System.Threading.Tasks;
using Xunit;

namespace AuditService.Tests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            var builder = new AuditLogMessageDtoBuilder();
            var dto = builder.Get();
            var topik = "test.payment.transactions-events";
            var auditLog = Helper.SerializeToString(dto);
            var producerTest = new KafkaProducerTest();
            await producerTest.KafkaProducerStart(topik, auditLog);
        }
    }    
}