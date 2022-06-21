using AuditService.Kafka.Services.Health;
using AuditService.Setup.ModelProviders;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;
using Tolar.Kafka;
using Tolar.Redis;
using KafkaProducer = AuditService.Kafka.Kafka.KafkaProducer;

namespace AuditService.WebApi;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ResponseHttpCodeModelProvider>());
        services.AddSingleton<IKafkaProducer, KafkaProducer>();
        services.AddSingleton<IRedisRepository, RedisRepository>();


        services
            .AddSingleton<HealthService>()
            .AddSingleton<IHealthMarkService>(x => x.GetRequiredService<HealthService>())
            .AddSingleton<IHealthService>(x => x.GetRequiredService<HealthService>());

        Providers.DiConfigure.RegisterServices(services);
    }
}