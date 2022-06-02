using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using Xunit;

namespace AuditService.IntegrationTests
{
    public class Test2
    {
        [Fact]
        public async Task Producer_AuditLog_Kafka222()
        {
            var cc = 1;
            var serviceCollection = new ServiceCollection();
        }
    }
}
