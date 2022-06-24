using System.Text.Json.Serialization;
using AuditService.Utility.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Setup.Extensions;

public static class ControllersServiceExtension
{
    public static void AddControllersExtension(this IServiceCollection services)
    {
        services
            .AddControllers(options => { options.Filters.Add<LoggingActionFilter>(); })
            .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    }
}