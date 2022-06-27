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
        builder.Configuration.AddEnvironmentVariables();

        builder.SetEnvironment();

        builder.Configuration.AddJsonFile("config/aus.api.appsettings.json", $"config/aus.api.env.{builder.Environment.EnvironmentName.ToLower()}.json", builder.Environment);
        builder.Configuration.AddJsonFile("config/aus.api.logger.json", builder.Environment);
        builder.Configuration.AddJsonFile($"config/aus.api.logger.{builder.Environment.EnvironmentName.ToLower()}.json", builder.Environment);
    }
}