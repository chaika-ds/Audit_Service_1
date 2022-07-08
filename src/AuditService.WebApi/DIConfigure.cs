using AuditService.Setup.ModelProviders;
using KIT.Kafka;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;
using Tolar.Redis;

namespace AuditService.WebApi;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services, string environmentName)
    {
        services.AddSingleton<IRedisRepository, RedisRepository>();
        services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        services.AddSingleton<ITokenService, TokenService>();
        services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ResponseHttpCodeModelProvider>());
        Handlers.DiConfigure.RegisterServices(services);
        services.ConfigureKafka(environmentName);
    }
}