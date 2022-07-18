using AuditService.Setup.ModelProviders;
using KIT.Kafka;
using KIT.Redis;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;

namespace AuditService.WebApi;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services, string environmentName)
    {
        services.ConfigureRedis();
        services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        services.AddSingleton<ITokenService, TokenService>();
        services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ResponseHttpCodeModelProvider>());
        Handlers.DiConfigure.RegisterServices(services);
        services.ConfigureKafka(environmentName);
    }
}