using AuditService.Common.Models.Domain;
using AuditService.IntegrationTests.EventProducer.Builder;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Tolar.Kafka;
using KafkaProducer = AuditService.Kafka.Kafka.KafkaProducer;

namespace AuditService.IntegrationTests.EventProducer.Settings
{
    public static class DIConfigure
    {
        public static IServiceCollection AddTestServices(this IServiceCollection services)
        {
            services.AddSettings<IDirectorSettings, IKafkaSettings, AppSettings>();

            services
                .AddLogging()
                .AddSingleton(services)
                .AddSingleton<IBuilderDto<AuditLogTransactionDomainModel>, AuditLogMessageDtoBuilder>()
                .AddSingleton<IBuilderDto<IdentityUserDomainModel>, IdentityUserDtoBuilder>()
                .AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<KafkaProducer>>())
                .AddSingleton<KafkaProducer>()
                .AddSingleton<IDirector, Director>();

            return services;
        }
    }
}
