using AuditService.Data.Domain.Dto;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace AuditService.IntegrationTests
{
    public class AuditLogProducerTest
    {
        [Fact]
        public async Task Producer_AuditLog_Kafka123()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddTestServices();
            
            var serviceProvider = serviceCollection.BuildServiceProvider();
            

            var service = serviceProvider.GetService<IDirector>();
            await service?.GenerateDto<AuditLogTransactionDto>();
        }
    }
}
