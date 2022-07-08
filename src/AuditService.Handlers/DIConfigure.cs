using System.Reflection;
using AuditService.Handlers.PipelineBehaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Handlers;

public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CachePipelineBehavior<,>));
    }
}