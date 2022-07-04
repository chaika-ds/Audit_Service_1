using System.Reflection;
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
    }
}