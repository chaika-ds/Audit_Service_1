﻿using System.Net;
using AuditService.Common.Exceptions;
using AuditService.Common.Extensions;
using AuditService.Common.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Tolar.Authenticate;

namespace AuditService.Setup.Middleware;

/// <summary>
///     Middleware handle for exceptions
/// </summary>
public class AppMiddlewareException
{
    private readonly IWebHostEnvironment _environment;
    private readonly RequestDelegate _next;

    public AppMiddlewareException(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (SsoForbidException ex)
        {
            await HandleExceptionAsync(context, ex, HttpStatusCode.Forbidden);
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
        var resultObject = new ProblemDetails
        {
            Status = (int)code,
            Title = exp.Message,
            Instance = context.Request.Path,
            Type = code.ToString()
        };

        if (!_environment.IsProduction())
            resultObject.Detail = exp.FullMessage();

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        await context.Response.WriteAsync(resultObject.SerializeToString());
    }
}