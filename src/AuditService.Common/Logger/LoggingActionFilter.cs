using System.Text;
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
            var requestPath = context.HttpContext?.Request?.Path.Value;

            try
            {
                var requestBody = GetRequestBodyAsString(context);                

                _logger.LogInformation($"Start execution request: {requestPath} request body: {requestBody}");

                await next();

                _logger.LogInformation($"Finish execution request: {requestBody}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while request: {requestPath}\r\n {ex.Message}");
            }
        }

        private string GetRequestBodyAsString(ActionExecutingContext context)
        {
            var builder = new StringBuilder();

            foreach (var element in context.ActionArguments)
                builder.Append(JsonHelper.SerializeToString(element.Value));

            return builder.ToString();
        }
    }
}