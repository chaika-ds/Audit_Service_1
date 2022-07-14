using KIT.NLog.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AuditService.Handlers.PipelineBehaviors;

/// <summary>
///     Handler pipeline element for request logging
/// </summary>
/// <typeparam name="TRequest">Request type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public class LogPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse> where TResponse : class
{
    private readonly ILogger<TRequest> _logger;

    public LogPipelineBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    /// <summary>
    ///     Perform request prehandling for logging
    /// </summary>
    /// <param name="request">Request for logging</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <param name="next">The request handling pipeline delegate</param>
    /// <returns>Response after request handling</returns>
    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        try
        {
            var response = await next();
            _logger.LogTrace("Handling request completed successfully", GenerateContextModel(request, response));
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, "Handling request ended with an error", GenerateContextModel(request));
            throw;
        }
    }

    /// <summary>
    ///     Generate a context model for logging
    /// </summary>
    /// <param name="request">Request for handling</param>
    /// <param name="response">Response after request handling</param>
    /// <returns>Context model for logging</returns>
    private static object GenerateContextModel(TRequest request, TResponse? response = null)
        => new
        {
            RequestName = typeof(TRequest).Name,
            Request = request,
            Response = response
        };
}