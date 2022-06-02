using AuditService.Common.Kafka;
using AuditService.Data.Domain.Dto;
using bgTeam.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AuditService.IntegrationTests
{
    public static class DIConfigure
    {
        public static IServiceCollection AddTestServices(this IServiceCollection services)
        {
            services.AddSettings<IDirectorSettings, IKafkaSettings, AppSettings>();

            services
                .AddLogging()
                .AddSingleton(services)
                .AddSingleton<IBuilderDto<AuditLogTransactionDto>, BuilderBase<AuditLogTransactionDto>>()
                .AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<KafkaProducer>>())
                .AddSingleton<KafkaProducer>()
                .AddSingleton<IDirector, Director>();


            return services;
        }

        //public void ServiceConfiguration()
        //{
        //    var serviceCollection = new ServiceCollection();
        //    serviceCollection.AddTestServices();

        //    var serviceProvider = serviceCollection.BuildServiceProvider();

        //    var service = serviceProvider.GetService<RequestHandler>();
        //}
    }
}
