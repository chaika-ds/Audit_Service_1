using System.Reflection;
using bgTeam.Extensions;
using KIT.RocketChat.ApiClient;
using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.Commands.Authorization;
using KIT.RocketChat.Commands.PostBufferedTextMessage;
using KIT.RocketChat.Settings;
using KIT.RocketChat.Settings.Interfaces;
using KIT.RocketChat.Storage;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;

namespace KIT.RocketChat;

/// <summary>
///     RocketChat configurator
/// </summary>
public static class RocketChatConfigurator
{
    /// <summary>
    ///     Configure RocketChat. Register services and settings.
    /// </summary>
    /// <param name="services">Services сollection</param>
    public static void ConfigureRocketChat(this IServiceCollection services)
    {
        services.RegisterSettings();
        services.RegisterServices();
    }

    /// <summary>
    /// Register settings
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterSettings(this IServiceCollection services)
    {
        services.AddSettings<IRocketChatApiSettings, RocketChatApiSettings>();
        services.AddSettings<IRocketChatMethodsSettings, RocketChatMethodsSettings>();
        services.AddSettings<IRocketChatStorageSettings, RocketChatStorageSettings>();
    }

    /// <summary>
    /// Register services
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterServices(this IServiceCollection services)
    {
        services.AddTransient(provider =>
        {
            var apiSettings = provider.GetRequiredService<IRocketChatApiSettings>();
            var baseUrl = $"{apiSettings.BaseApiUrl}/{apiSettings.ApiVersion}/";
            return new RestClient(baseUrl);
        });

        services.AddTransient<IRocketChatApiClient, RocketChatApiClient>();
        services.AddTransient<IRocketChatStorage, RedisCacheStorage>();
        services.RegisterRocketChatApiMethods();
        services.RegisterRocketChatCommands();
    }

    /// <summary>
    /// Register RocketChat commands
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterRocketChatCommands(this IServiceCollection services)
    {
        services.AddTransient<IAuthorizationCommand, AuthorizationCommand>();
        services.AddTransient<IPostBufferedMessageCommand, PostBufferedMessageCommand>();
    }

    /// <summary>
    ///     Register RocketChat api methods
    /// </summary>
    /// <param name="services">Services сollection</param>
    private static void RegisterRocketChatApiMethods(this IServiceCollection services) =>
        (from type in Assembly.GetAssembly(typeof(IBaseMethod))?.GetTypes()
            let genericType = type.BaseType
            where !type.IsAbstract && !type.IsInterface &&
                  genericType is { IsGenericType: true } && genericType.GetGenericTypeDefinition() == typeof(BaseMethod<,>)
            select type).ToList().ForEach(serviceType => services.AddTransient(serviceType));
}