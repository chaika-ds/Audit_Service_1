using AuditService.Kafka.KafkaTest;
using AuditService.Data.Domain.Dto;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using AuditService.Data.Domain.Domain;
using AuditService.IntegrationTests.EventProducer.Builder;
using AuditService.IntegrationTests.EventProducer.Settings;
using Xunit;
using AuditService.Utility.Helpers;

namespace AuditService.IntegrationTests
{
    public class ProducerAuditLogGenerator
    {
        /// <summary>
        /// Generqator for number audit log Kafka messages
        /// </summary>
        [Fact]
        public async Task KafkaProducer_AuditLog_GeneratorAsync()
        {
            var generatedMessages = 2;
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTestServices();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service = serviceProvider.GetService<IDirector>();
            await service?.GenerateDtoAsync<AuditLogTransactionDomainModel>(generatedMessages);
        }

        /// <summary>
        /// Simple producer for audit log Kafka messages
        /// </summary>
        [Fact]
        public async Task Kafka_AuditLog_ProducerAsync()
        {
            var builder = new AuditLogMessageDtoBuilder();
            var topicName = "uat.auditlog.messages";
            var dto = builder.Get();
            var topik = topicName;
            var auditLog = JsonHelper.SerializeToString(dto);
            var producerTest = new KafkaProducerTest();
            await producerTest.KafkaProducerStartAsync(topik, auditLog);
        }
    }
}