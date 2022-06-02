using AuditService.Common.Kafka;
using AuditService.Data.Domain.Dto;
using AuditService.Test.EventProducer.Builder;
using bgTeam.DataAccess;



namespace AuditService.IntegrationTests.Settings
{
    internal static class DiConfigure
    {

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddSettings<IKafkaConsumerSettings, IDirectorSettings, AppSettings1>();

            services.AddSingleton<KafkaProducer>();

            services
                .AddSingleton(services)
                .AddSingleton<IBuilderDto<AuditLogMessageDto>, BuilderBase<AuditLogMessageDto>>();




            return services;
        }
        public void ServiceConfiguration()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddServices();

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service = serviceProvider.GetService<RequestHandler>();
        }
    }
}
