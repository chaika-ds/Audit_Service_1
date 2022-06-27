using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using AuditService.Setup.Extensions;

namespace AuditService.Setup.ServiceConfigurations;

/// <summary>
///     Config files and environments configurations
/// </summary>
public static class ConfigFileConfiguration
{
    /// <summary>
    ///     Adds the JSON configuration and EnvironmentVariables
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public static void AddConfigs(this WebApplicationBuilder builder)
    {
        var environmentName = builder.Environment.EnvironmentName.ToLower();

        builder.Configuration.AddEnvironmentVariables();
        builder.Configuration.AddJsonFile("config/aus.api.appsettings.json", $"config/aus.api.env.{environmentName}.json", builder.Environment);
        builder.Configuration.AddJsonFile($"config/aus.api.logger.{environmentName}.json", builder.Environment);
    }
}