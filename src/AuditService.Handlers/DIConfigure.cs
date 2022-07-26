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
        services.RegisterPipelineBehaviors(typeof(LogPipelineBehavior<,>), attribute => attribute.UseLogging);
        services.RegisterPipelineBehaviors(typeof(CachePipelineBehavior<,>), attribute => attribute.UseCache);
    }
}