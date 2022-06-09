using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using AuditService.Common.Helpers;
using AuditService.Data.Domain.Exceptions;

namespace AuditService.WebApi.Middleware;

/// <summary>
///     Middleware handle for exceptions
/// </summary>
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

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Unauthorized);
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

    /// <summary>
    ///     Create error response
    /// </summary>
    private async Task HandleExceptionAsync(HttpContext context, Exception exp, HttpStatusCode code)
    {
        _logger.LogError(exp, $"{code}: {exp.Message}\r\n{exp.StackTrace}");

        var resultObject = new ProblemDetails
        {
            Status = (int)code,
            Title = exp.Message,
            Instance = context.Request.Path,
            Type = code.ToString()
        };

        if (!_environment.IsProduction())
            resultObject.Detail = exp.StackTrace;

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(JsonHelper.SerializeToString(resultObject));
    }
}
