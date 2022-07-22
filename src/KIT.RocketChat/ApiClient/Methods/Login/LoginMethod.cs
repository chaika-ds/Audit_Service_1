using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.ApiClient.Methods.Login.Models;
using KIT.RocketChat.Settings.Interfaces;
using RestSharp;

namespace KIT.RocketChat.ApiClient.Methods.Login;

/// <summary>
///     API RocketChat method for login
/// </summary>
public class LoginMethod : BaseMethod<LoginRequest, LoginResponse>
{
    public LoginMethod(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get route
    /// </summary>
    /// <param name="methods">Methods to select</param>
    /// <param name="request">Request model</param>
    /// <returns>The route to execute the request</returns>
    protected override string GetRoute(IRocketChatMethodsSettings methods, LoginRequest request) => methods.LoginMethod!;

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
    protected override object GetBody(LoginRequest request) => request;
}