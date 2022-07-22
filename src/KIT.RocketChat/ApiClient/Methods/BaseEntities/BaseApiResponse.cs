using System.Net;
using RestSharp;

namespace KIT.RocketChat.ApiClient.Methods.BaseEntities;

/// <summary>
///     Base response from api RocketChat
/// </summary>
/// <typeparam name="TResult">ContentType</typeparam>
public class BaseApiResponse<TResult>
{
    public BaseApiResponse(TResult content, HttpStatusCode statusCode, ResponseStatus responseStatus)
    {
        IsSuccess = true;
        Content = content;
        StatusCode = statusCode;
        ResponseStatus = responseStatus;
    }

    public BaseApiResponse(string? errorMessage, HttpStatusCode statusCode, ResponseStatus responseStatus)
    {
        IsSuccess = false;
        ErrorMessage = errorMessage;
        StatusCode = statusCode;
        ResponseStatus = responseStatus;
    }

    public BaseApiResponse(string errorMessage)
    {
        IsSuccess = false;
        StatusCode = HttpStatusCode.BadRequest;
        ResponseStatus = ResponseStatus.Error;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    ///     Flag indicating the success of the operation
    /// </summary>
    public bool IsSuccess { get; }

    /// <summary>
    ///     Http code
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    ///     Status of the request. Will return Error for transport errors.
    ///     HTTP errors will still return ResponseStatus.Completed, check StatusCode instead
    /// </summary>
    public ResponseStatus ResponseStatus { get; }

    /// <summary>
    ///     Content
    /// </summary>
    public TResult? Content { get; }

    /// <summary>
    ///     Error message
    /// </summary>
    public string? ErrorMessage { get; }
}