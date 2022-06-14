using AuditService.Kafka.Kafka;
using AuditService.Data.Domain.Dto;
using AuditService.IntegrationTests.EventProducer.Builder;
using bgTeam.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using AuditService.Kafka.Settings;

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
                .AddSingleton<IBuilderDto<AuditLogTransactionDto>, AuditLogMessageDtoBuilder>()
                .AddSingleton<IBuilderDto<IdentityUserDto>, IdentityUserDtoBuilder>()
                .AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<KafkaProducer>>())
                .AddSingleton<KafkaProducer>()
                .AddSingleton<IDirector, Director>();

            return services;
        }
    }
}
