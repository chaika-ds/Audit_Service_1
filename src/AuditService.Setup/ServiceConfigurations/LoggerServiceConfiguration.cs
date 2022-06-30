using AuditService.Utility.Logger;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AuditService.Setup.ServiceConfigurations;

/// <summary>
///     Logger service configuration
/// </summary>
public static class LoggerServiceConfiguration
{
    /// <summary>
    ///     Adds customer logger provider
    /// </summary>
    public static void AddLogger(this WebApplicationBuilder builder)
    {
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Logging.AddAuditServiceLogger(options =>
        {
            builder.Configuration.Bind(options);
            options.Channel = builder.Environment.EnvironmentName.ToLower();
        });
    }
}