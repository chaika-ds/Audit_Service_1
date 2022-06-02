using AuditService.Common;
using AuditService.Common.Kafka;
using AuditService.Common.KafkaTest;
using AuditService.Data.Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace AuditService.IntegrationTests
{
    public class AuditLogProducerTest
    {
        /// <summary>
        /// Integration test for generating audit log messages to Kafka
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task KafkaGenerator_AuditLog_Async()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTestServices();

            var serviceProvider = serviceCollection.BuildServiceProvider();


            var service = serviceProvider.GetService<IDirector>();
            await service?.GenerateDto<AuditLogTransactionDto>();
        }
    }
}
