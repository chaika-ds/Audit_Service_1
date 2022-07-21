namespace KIT.RocketChat.ApiClient.Methods.BaseEntities;

/// <summary>
///     Base request for api RocketChat
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
public class BaseApiRequest<TRequest> where TRequest : class
{
    public BaseApiRequest(TRequest request, AuthData? authData = null)
    {
        Request = request;
        AuthData = authData;
    }

    /// <summary>
    ///     Request to sent
    /// </summary>
    public TRequest Request { get; set; }

    /// <summary>
    ///     RocketChat authentication data
    ///     Indicate if the method requires authentication
    /// </summary>
    public AuthData? AuthData { get; set; }
}