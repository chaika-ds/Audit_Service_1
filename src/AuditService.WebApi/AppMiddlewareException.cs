using AuditService.Common;
using AuditService.Common.Excaptions;
using AuditService.Common.Logger;
using Newtonsoft.Json;
using System.Net;

namespace AuditService.WebApi
{
    public class AppMiddlewareException
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<AppMiddlewareException> _logger;
        private readonly RequestDelegate _next;

        public AppMiddlewareException(RequestDelegate next, ILogger<AppMiddlewareException> logger, IWebHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BadRequestException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (ArgumentException ex)
            {
                await HandleExceptionAsync(context, ex, HttpStatusCode.BadRequest);
            }
            catch (AggregateException exp)
            {
                await HandleExceptionAsync(context, exp.GetBaseException(), HttpStatusCode.InternalServerError);
            }
            catch (JsonSerializationException exp)
            {
                await HandleExceptionAsync(context, exp, HttpStatusCode.BadRequest);
            }
            catch (Exception exp)
            {
                await HandleExceptionAsync(context, exp, HttpStatusCode.InternalServerError);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exp, HttpStatusCode code)
        {
            _logger.LogError(exp, $"{code}: {exp.Message}\r\n{exp.StackTrace}");

            var resultObject = new ErrorResponseDto
            {
                StatusCode = code,
                Message = exp.Message
            };

            if (!_environment.IsProduction())
                resultObject.StackTrace = exp.StackTrace;

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(Helper.SerializeToString(resultObject));
        }
    }
}
