using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using Microsoft.Extensions.DependencyInjection;

namespace KIT.RocketChat.ApiClient;

/// <summary>
///     RocketChat client api
/// </summary>
internal class RocketChatApiClient : IRocketChatApiClient
{
    private readonly IServiceProvider _serviceProvider;

    public RocketChatApiClient(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     Get api method
    /// </summary>
    /// <typeparam name="TMethod">Method type</typeparam>
    /// <returns>Api method</returns>
    public TMethod GetMethod<TMethod>() where TMethod : IBaseMethod => _serviceProvider.GetRequiredService<TMethod>();
}