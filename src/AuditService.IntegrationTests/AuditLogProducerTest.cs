using AuditService.Common;
using AuditService.Common.Kafka;
using AuditService.Common.KafkaTest;
using AuditService.Data.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Xunit;

namespace AuditService.IntegrationTests
{
    public class AuditLogProducerTest
    {
        //private readonly KafkaProducer _producer;
        //private readonly IBuilderDto<AuditLogMessageDto> _builder;
        //private readonly IDirector _director;

        //public AuditLogProducerTest(KafkaProducer producer,
        //    [FromServices] IBuilderDto<AuditLogMessageDto> builder,
        //     [FromServices] IDirector director)
        //{
        //    _producer = producer;
        //    _builder = builder;
        //    _director = director;
        //}

        [Fact]
        public void Producer_AuditLog_Kafka()
        {
            var builder = new AuditLogMessageDtoBuilder();
            var dto = builder.Get();
            var topik = "test.payment.transactions-events";
            var auditLog = Helper.SerializeToString(dto);
            var producerTest = new KafkaProducerTest();
             producerTest.KafkaProducerStart(topik, auditLog);        }
    }
}
