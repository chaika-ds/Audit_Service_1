using System.Net;
using AuditService.Common.Consts;
using KIT.NLog.Extensions;
using KIT.RocketChat.ApiClient.Consts;
using KIT.RocketChat.Settings.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RestSharp;
using mediaType = System.Net.Mime.MediaTypeNames.Application;

namespace KIT.RocketChat.ApiClient.Methods.BaseEntities;

/// <summary>
///     RocketChat api method base class
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public abstract class BaseMethod<TRequest, TResponse> : IBaseMethod where TRequest : class
{
    private readonly IRocketChatMethodsSettings _chatMethodsSettings;
    private readonly ILogger<BaseMethod<TRequest, TResponse>> _logger;
    private readonly RestClient _restClient;
    private readonly IRocketChatApiSettings _rocketChatApiSettings;

    protected BaseMethod(IServiceProvider serviceProvider)
    {
        _restClient = serviceProvider.GetRequiredService<RestClient>();
        _logger = serviceProvider.GetRequiredService<ILogger<BaseMethod<TRequest, TResponse>>>();
        _chatMethodsSettings = serviceProvider.GetRequiredService<IRocketChatMethodsSettings>();
        _rocketChatApiSettings = serviceProvider.GetRequiredService<IRocketChatApiSettings>();
    }

    /// <summary>
    ///     Execute api method
    /// </summary>
    /// <param name="requestModel">Request model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response/Result of the request</returns>
    public async Task<BaseApiResponse<TResponse>> ExecuteAsync(BaseApiRequest<TRequest> requestModel, CancellationToken cancellationToken)
    {
        try
        {
            if (_rocketChatApiSettings.IsActive! == false)
                return new BaseApiResponse<TResponse>("RocketChat is disabled. To resume the chat, enable the chat in the settings.");

            var request = CreateRequest(requestModel);
            var response = await _restClient.ExecuteAsync(request, cancellationToken);

            if (!response.IsSuccessful || response.StatusCode != HttpStatusCode.OK || string.IsNullOrEmpty(response.Content))
                return new BaseApiResponse<TResponse>(response.ErrorMessage, response.StatusCode, response.ResponseStatus);

            var responseContent = JsonConvert.DeserializeObject<TResponse>(response.Content)!;
            return new BaseApiResponse<TResponse>(responseContent, response.StatusCode, response.ResponseStatus);
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Call to API threw an exception",
                new { route = GetRoute(_chatMethodsSettings, requestModel.Request), requestModel });
            return new BaseApiResponse<TResponse>(ex.Message);
        }
    }

    /// <summary>
    ///     Get route
    /// </summary>
    /// <param name="methods">Methods to select</param>
    /// <param name="request">
    ///     Request model, for generating the route
    ///     (if necessary, for example, if the request is a get)
    /// </param>
    /// <returns>The route to execute the request</returns>
    protected abstract string GetRoute(IRocketChatMethodsSettings methods, TRequest request);

    /// <summary>
    ///     Get method type
    /// </summary>
    /// <returns>Method type</returns>
    protected abstract Method GetMethod();

    /// <summary>
    ///     Get the body of the request, if it exists and needs to be sent
    /// </summary>
    /// <param name="request">Request model</param>
    /// <returns>Body of the request</returns>
    protected abstract object? GetBody(TRequest request);

    /// <summary>
    ///     Create a request to send
    /// </summary>
    /// <param name="requestModel">Request model</param>
    /// <returns>Request to send</returns>
    private RestRequest CreateRequest(BaseApiRequest<TRequest> requestModel)
    {
        var request = new RestRequest(GetRoute(_chatMethodsSettings, requestModel.Request), GetMethod());
        request.AddHeader(HeaderNameConst.Accept, mediaType.Json);
        request.AddHeader(HeaderNameConst.ContentType, mediaType.Json);

        if (requestModel.AuthData is not null)
        {
            request.AddHeader(RocketChatApiConst.XUserId, requestModel.AuthData.UserId);
            request.AddHeader(RocketChatApiConst.XAuthToken, requestModel.AuthData.AuthToken);
        }

        var body = GetBody(requestModel.Request);

        if (body is not null)
            request.AddJsonBody(JsonConvert.SerializeObject(body));

        return request;
    }
}