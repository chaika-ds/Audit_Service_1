using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace AuditService.Common.Logger
{
    /// <summary>
    /// Logging attribute for incoming requests
    /// </summary>
    public class LoggingActionFilter : IAsyncActionFilter
    {
        private readonly ILogger _logger;
        public LoggingActionFilter(ILogger logger)
        {
            _logger = logger;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var userId = GetUserId(context.HttpContext);
            var isOnRequest = true;
            var requestPath = context.HttpContext.Request.Path.Value;

            try
            {
                //before execution                
                var requestBody = GetRequestBodyAsString(context);                

                _logger.LogInformation($"Start execution request for user: {userId}, request path: {requestPath}" +
                    $" request body: {requestBody}, task is on request: {isOnRequest}");

                var result = await next();

                //after execution
                isOnRequest = false;

                _logger.LogInformation($"Finish execution request for user: {userId}, request body: {requestBody}, task is on request: {isOnRequest}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while request: {requestPath}\r\n {ex.Message}");
            }
        }

        private string GetUserId(HttpContext context)
        {
            var claimsIdentity = context?.User?.Identity as ClaimsIdentity;
            var claim = claimsIdentity?.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        private string GetRequestBodyAsString(ActionExecutingContext context)
        {
            var result = string.Empty;

            foreach (var element in context.ActionArguments)
            {
                result += Helper.SerializeToString(element.Value);
            }

            return result;
        }
    }
}
