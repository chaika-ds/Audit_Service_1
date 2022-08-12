using AuditService.Common.Consts;
using AuditService.Common.Contexts;
using Microsoft.AspNetCore.Http;

namespace AuditService.Setup.Middleware;

/// <summary>
///     Middleware for set request context
/// </summary>
public class SetRequestContextMiddleware
{
    private readonly RequestDelegate _next;

    public SetRequestContextMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    ///     Invoke middleware
    /// </summary>
    /// <param name="context">Http context</param>
    /// <param name="requestContext">Request context</param>
    /// <returns>Task execution result</returns>
    public async Task InvokeAsync(HttpContext context, RequestContext requestContext)
    {
        if (context.Request.Headers.TryGetValue(HeaderNameConst.XNodeId, out var xNodeId))
            requestContext.XNodeId = xNodeId;

        if (context.Request.Headers.TryGetValue(HeaderNameConst.Language, out var language))
            requestContext.Language = language;

        await _next(context);
    }
}