using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using AuditService.Common.Models.Domain;
using AuditService.IntegrationTests.EventProducer.Builder;
using AuditService.IntegrationTests.EventProducer.Settings;
using Xunit;

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
    }
}