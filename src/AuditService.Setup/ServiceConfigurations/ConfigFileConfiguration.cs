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
    public static async Task AddConfigsAsync(this WebApplicationBuilder builder)
    {
        builder.Configuration.AddEnvironmentVariables();

        builder.SetEnvironment();

        await builder.Configuration.AddJsonFileAsync("config/aus.api.appsettings.json", $"config/aus.api.env.{builder.Environment.EnvironmentName.ToLower()}.json", builder.Environment);
        await builder.Configuration.AddJsonFileAsync("config/aus.api.logger.json", builder.Environment);
    }
}