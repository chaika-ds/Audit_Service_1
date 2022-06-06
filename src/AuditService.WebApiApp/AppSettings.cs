﻿using AuditService.Common.Health;
using AuditService.Common.Kafka;
using bgTeam.DataAccess;
using Microsoft.Extensions.Configuration;

namespace AuditService.WebApiApp
{
    public class AppSettings : IConnectionSetting, IKafkaConsumerSettings, IHealthSettings, IProjectSettings
    {
        public string ConnectionString { get; set; }

        public int MaxTimeoutMsec { get; set; }
        public int MaxThreadsCount { get; set; }

        public Dictionary<string, string> Config { get; set; }

        public int CriticalErrorsCount { get; set; }
        public int ForPeriodInSec { get; set; }
        public string ServiceCategoriesJsonPath { get; set; }
        public string SsoBaseUrl { get; set; }

        public AppSettings(IConfiguration config)
        {
            MaxTimeoutMsec = int.Parse(config["Kafka:MaxTimeoutMsec"]);
            MaxThreadsCount = int.Parse(config["Kafka:MaxThreadsCount"]);

            Config = config.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            ApplyKafkaAliases(config, Config);

            CriticalErrorsCount = int.Parse(config["Health:CriticalErrorsCount"]);
            ForPeriodInSec = int.Parse(config["Health:ForPeriodInSec"]);
            
            ServiceCategoriesJsonPath = config["ProjectSettings:ServiceCategoriesJsonPath"];
            SsoBaseUrl = config["ProjectSettings:SsoBaseUrl"];
        }

        private static void ApplyKafkaAliases(IConfiguration configuration, Dictionary<string, string> config)
        {
            var aliases = configuration.GetSection("Kafka:Aliases").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            foreach (var item in aliases)
            {
                var value = configuration[$"Kafka:{item.Key}"];

                if (!string.IsNullOrEmpty(value))
                {
                    config[item.Value] = value;
                }
            }
        }
    }
}