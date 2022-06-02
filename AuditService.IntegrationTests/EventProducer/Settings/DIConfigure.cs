using AuditService.Common.Kafka;
using AuditService.Data.Domain.Dto;
using bgTeam.Extensions;
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
                .AddSingleton<IBuilderDto<AuditLogTransactionDto>, AuditLogMessageDtoBuilder>()
                .AddSingleton<IBuilderDto<IdentityUserDto>, IdentityUserDtoBuilder>()
                .AddSingleton<ILogger>(svc => svc.GetRequiredService<ILogger<KafkaProducer>>())
                .AddSingleton<KafkaProducer>()
                .AddSingleton<IDirector, Director>();

            return services;
        }
    }
}
