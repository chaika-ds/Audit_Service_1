using System.Text.Json.Serialization;
using AuditService.Utility.Logger.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Setup.ServiceConfigurations;

/// <summary>
///     Service configuration of controlles
/// </summary>
public static class ControllersServiceConfiguration
{
    /// <summary>
    ///     Adds services for controllers to the specified <see cref="T:Microsoft.Extensions.DependencyInjection.IServiceCollection" />.
    ///     This method append additional filters for views.
    /// </summary>
    public static void AddControllersWithFilters(this IServiceCollection services)
    {
        services
            .AddControllers(options => { options.Filters.Add<LoggingActionFilter>(); })
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }
}