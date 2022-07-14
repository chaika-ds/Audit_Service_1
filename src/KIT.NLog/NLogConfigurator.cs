using AuditService.Common.Consts;
using AuditService.Setup.Extensions;
using KIT.NLog.Consts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using NLog.Web;
using LogLevel = NLog.LogLevel;

namespace KIT.NLog;

/// <summary>
///     NLog configurator
/// </summary>
public static class NLogConfigurator
{
    /// <summary>
    ///     Configure NLog. Register services and settings.
    /// </summary>
    /// <param name="builder">Application Builder</param>
    public static void ConfigureNLog(this WebApplicationBuilder builder)
    {
        builder.SetupLogManager();
        builder.Logging.ClearProviders();
        builder.Host.UseNLog();
        builder.Environment.SetupLoggerTemplate();
    }

    /// <summary>
    ///     Set up NLog logger manager
    /// </summary>
    /// <param name="builder">Application Builder</param>
    private static void SetupLogManager(this WebApplicationBuilder builder)
    {
        var config = new ConfigurationBuilder();
        config.AddJsonFile("config/aus.api.logger.json", builder.Environment);
        var configurationRoot = config.AddEnvironmentVariables().Build();
        LogManager.Setup().LoadConfigurationFromSection(configurationRoot);
    }

    /// <summary>
    ///     Set up a logger template by environment
    /// </summary>
    /// <param name="environment">Environment application</param>
    private static void SetupLoggerTemplate(this IHostEnvironment environment)
    {
        GlobalDiagnosticsContext.Set(LogTemplateConst.ChannelField, environment.EnvironmentName);

        if (NeedToChangeGlobalThreshold(environment.EnvironmentName))
            LogManager.GlobalThreshold = LogLevel.Info;
    }

    /// <summary>
    ///     Check that you need to change the minimum logging level
    ///     depending on the environment
    /// </summary>
    /// <param name="environmentName">Environment name</param>
    /// <returns>Need to change the minimum logging level</returns>
    private static bool NeedToChangeGlobalThreshold(string environmentName) 
        => string.Equals(environmentName, EnvironmentNameConst.Demo, StringComparison.CurrentCultureIgnoreCase) ||
           string.Equals(environmentName, EnvironmentNameConst.Production, StringComparison.CurrentCultureIgnoreCase);
}