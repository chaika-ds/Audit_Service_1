using AuditService.Common.Helpers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

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
            var isOnRequest = true;
            var requestPath = context.HttpContext.Request.Path.Value;

            try
            {
                //before execution                
                var requestBody = GetRequestBodyAsString(context);                

                _logger.LogInformation($"Start execution request: {requestPath}" +
                    $" request body: {requestBody}, task is on request: {isOnRequest}");

                var result = await next();

                //after execution
                isOnRequest = false;

                _logger.LogInformation($"Finish execution request: {requestBody}, task is on request: {isOnRequest}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while request: {requestPath}\r\n {ex.Message}");
            }
        }

        private string GetRequestBodyAsString(ActionExecutingContext context)
        {
            var result = string.Empty;

            foreach (var element in context.ActionArguments)
            {
                result += JsonHelper.SerializeToString(element.Value);
            }

            return result;
        }
    }
}
