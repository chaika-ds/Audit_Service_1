using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.ApiClient.Methods.PostMessage.Models;
using KIT.RocketChat.Settings.Interfaces;
using RestSharp;

namespace KIT.RocketChat.ApiClient.Methods.PostMessage;

/// <summary>
///     API RocketChat method for post message
/// </summary>
public class PostMessageMethod : BaseMethod<PostMessageRequest, PostMessageResponse>
{
    public PostMessageMethod(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get route
    /// </summary>
    /// <param name="methods">Methods to select</param>
    /// <param name="request">Request model</param>
    /// <returns>The route to execute the request</returns>
    protected override string GetRoute(IRocketChatMethodsSettings methods, PostMessageRequest request) => methods.PostMessageMethod!;

    /// <summary>
    ///     Get method type
    /// </summary>
    /// <returns>Method type</returns>
    protected override Method GetMethod() => Method.Post;

    /// <summary>
    ///     Get the body of the request, if it exists and needs to be sent
    /// </summary>
    /// <param name="request">Request model</param>
    /// <returns>Body of the request</returns>
    protected override object GetBody(PostMessageRequest request) => request;
}