﻿using AuditService.Common.Contexts;
using AuditService.Localization;
using AuditService.SettingsService;
using AuditService.Setup.ModelProviders;
using KIT.Kafka;
using KIT.Minio;
using KIT.Redis;
using KIT.RocketChat;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Tolar.Authenticate;
using Tolar.Authenticate.Impl;

namespace AuditService.WebApi;

/// <summary>
///     DI configuration for api
/// </summary>
public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(this IServiceCollection services, string environmentName)
    {
        services.ConfigureRedis();
        services.ConfigureLocalization();
        services.ConfigureSettingsService();
        services.ConfigureRocketChat();
        services.ConfigureMinio();
        services.AddHttpClient<IAuthenticateService, AuthenticateService>();
        services.AddSingleton<ITokenService, TokenService>();
        services.TryAddEnumerable(ServiceDescriptor.Transient<IApplicationModelProvider, ResponseHttpCodeModelProvider>());
        Handlers.DiConfigure.RegisterServices(services);
        services.ConfigureKafka(environmentName);
        services.AddScoped<RequestContext>();
    }
}