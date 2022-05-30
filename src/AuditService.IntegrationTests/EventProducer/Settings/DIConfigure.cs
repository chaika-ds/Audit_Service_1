using AuditService.Common.Kafka;
using AuditService.Data.Domain.Dto;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;


namespace AuditService.IntegrationTests
{
    internal static class DiConfigure
    {
        //public static IServiceCollection AddServices(this IServiceCollection services)
        //{
        //    services.AddSettings<IDirectorSettings, IKafkaSettings, AppSettings>();

        //    services.AddSingleton<KafkaProducer>();

        //    services
        //        .AddSingleton(services)
        //        .AddSingleton<IBuilderDto<AuditLogMessageDto>, BuilderBase<AuditLogMessageDto>>()


        //        .AddSingleton<IDirector, Director>(); ;




        //    return services;
        //}

        //public void ServiceConfiguration()
        //{
        //    var serviceCollection = new ServiceCollection();
        //    serviceCollection.AddServices();

        //    var serviceProvider = serviceCollection.BuildServiceProvider();

        //    var service = serviceProvider.GetService<RequestHandler>();
        //}
    }
}
