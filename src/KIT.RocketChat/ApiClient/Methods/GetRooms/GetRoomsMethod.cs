using KIT.RocketChat.ApiClient.Methods.BaseEntities;
using KIT.RocketChat.ApiClient.Methods.GetRooms.Models;
using KIT.RocketChat.Settings.Interfaces;
using RestSharp;

namespace KIT.RocketChat.ApiClient.Methods.GetRooms;

/// <summary>
///     API RocketChat method for getting rooms
/// </summary>
public class GetRoomsMethod : BaseMethod<GetRoomsRequest, GetRoomsResponse>
{
    public GetRoomsMethod(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get route
    /// </summary>
    /// <param name="methods">Methods to select</param>
    /// <param name="request">Request model</param>
    /// <returns>The route to execute the request</returns>
    protected override string GetRoute(IRocketChatMethodsSettings methods, GetRoomsRequest request) =>
        request.UpdatedSince.HasValue ? $"{methods.GetRoomsMethod}?updatedSince={request.UpdatedSince.Value:yyyy-MM-ddTHH:mm:ss.sssZ}"
            : methods.GetRoomsMethod!;

    /// <summary>
    ///     Get method type
    /// </summary>
    /// <returns>Method type</returns>
    protected override Method GetMethod() => Method.Get;

    /// <summary>
    ///     Get the body of the request, if it exists and needs to be sent
    /// </summary>
    /// <param name="request">Request model</param>
    /// <returns>Body of the request</returns>
    protected override object? GetBody(GetRoomsRequest request) => null;
}