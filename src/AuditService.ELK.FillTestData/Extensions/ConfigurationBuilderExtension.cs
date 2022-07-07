using AuditService.Common.Consts;
﻿using AuditService.Setup.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace AuditService.ELK.FillTestData.Extensions
{
    /// <summary>
    /// Extension of configuration builder
    /// </summary>
    public static class ConfigurationBuilderExtension
    {
        /// <summary>
        /// Configure application configurations
        /// </summary>
        /// <param name="configurationBuilder">Сonfiguration builder</param>
        /// <param name="environment">Host environment</param>
        /// <returns>Сonfiguration builder</returns>
        public static IConfigurationBuilder ConfigureAppConfiguration(this IConfigurationBuilder configurationBuilder,
            IHostEnvironment environment)
        {
            environment.EnvironmentName = EnvironmentNameConst.Debug;
            configurationBuilder.AddJsonFile("appsettings.json");
            configurationBuilder.AddJsonFile("config/aus.api.appsettings.json",
                $"config/aus.api.env.{environment.EnvironmentName.ToLower()}.json", environment);
            configurationBuilder.AddJsonFile("config/aus.api.logger.json", environment);

            return configurationBuilder;
        }
    }
}