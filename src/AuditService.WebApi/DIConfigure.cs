using AuditService.Setup.ModelProviders;
using KIT.Kafka;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;
using Tolar.Kafka;
using Tolar.Redis;

namespace AuditService.WebApi;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services, string environmentName)
    {
        services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        services.AddSingleton<ITokenService>(provider => new TokenService(provider.GetService<IRedisRepository>(), provider.GetService<IAuthenticateService>()));
        services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ResponseHttpCodeModelProvider>());
        services.AddSingleton<IRedisRepository, RedisRepository>();
        Handlers.DiConfigure.RegisterServices(services);
        services.ConfigureKafka(environmentName);
    }
}