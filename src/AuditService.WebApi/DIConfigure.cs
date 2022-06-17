using AuditService.Kafka.Services.Health;
using AuditService.Kafka.Settings;
using AuditService.Setup;
using AuditService.Setup.Interfaces;
using AuditService.Setup.ModelProviders;
using bgTeam.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;
using Tolar.Kafka;
using Tolar.Redis;
using IKafkaSettings = AuditService.Kafka.Settings.IKafkaSettings;
using KafkaProducer = AuditService.Kafka.Kafka.KafkaProducer;

namespace AuditService.WebApi;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void Configure(IServiceCollection services)
    {
        services
            .TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ResponseHttpCodeModelProvider>());

        services
            .AddSettings<
                IKafkaSettings,
                IHealthSettings,
                IJsonData,
                IAuthenticateServiceSettings,
                IPermissionPusher,
                IElasticIndex,
                IRedisSettings,
                AppSetting>()
            .AddSingleton(services);
        
        services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        services.AddTransient<IRedisRepository, RedisRepository>();
        services.AddSingleton<IKafkaProducer, KafkaProducer>();

        services
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IHealthService>(x => x.GetRequiredService<HealthService>());

        Providers.DiConfigure.Configure(services);
    }
}