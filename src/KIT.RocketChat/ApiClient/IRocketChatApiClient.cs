using KIT.RocketChat.ApiClient.Methods.BaseEntities;

namespace KIT.RocketChat.ApiClient
{
    /// <summary>
    ///     RocketChat client api
    /// </summary>
    public interface IRocketChatApiClient
    {
        /// <summary>
        ///     Get api method
        /// </summary>
        /// <typeparam name="TMethod">Method type</typeparam>
        /// <returns>Api method</returns>
        TMethod GetMethod<TMethod>() where TMethod : IBaseMethod;
    }
}
