using System.Reflection;
using AuditService.Handlers.PipelineBehaviors;
using AuditService.Handlers.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Export.Services;
using Tolar.Export.Services.Impl;

namespace AuditService.Handlers;

/// <summary>
///     Configure request handler services
/// </summary>
public static class DiConfigure
{
    /// <summary>
    ///     Register custom services
    /// </summary>
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.RegisterPipelineBehaviors(typeof(LogPipelineBehavior<,>), attribute => attribute.UseLogging);
        services.RegisterPipelineBehaviors(typeof(ValidationPipelineBehavior<,>), attribute => attribute.UseValidation);
        services.RegisterPipelineBehaviors(typeof(CachePipelineBehavior<,>), attribute => attribute.UseCache);
        services.AddValidatorsFromAssemblyContaining<AuditLogDomainRequestValidator>(ServiceLifetime.Transient);
        services.RegisterExportServices();
    }

    /// <summary>
    ///     Register file export services
    /// </summary>
    /// <param name="services">Service collection</param>
    private static void RegisterExportServices(this IServiceCollection services)
    {
        services.AddScoped<IExportService, CsvExportService>();
        services.AddScoped<IExportService, ExcelExportService>();
        services.AddScoped<IExportFactory, ExportFactory>();
    }
}