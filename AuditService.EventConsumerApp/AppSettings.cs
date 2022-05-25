using AuditService.Common.Health;
using AuditService.Common.Kafka;
using bgTeam.DataAccess;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace AuditService.EventConsumerApp
{
    public class AppSettings : IConnectionSetting, IKafkaConsumerSettings, IHealthSettings
    {
        public string ConnectionString { get; set; }

        public int MaxTimeoutMsec { get; set; }
        public int MaxThreadsCount { get; set; }

        public Dictionary<string, string> Config { get; set; }

        public int CriticalErrorsCount { get; set; }
        public int ForPeriodInSec { get; set; }

        public AppSettings(IConfiguration config)
        {
            //ConnectionString = config.GetConnectionString("ReportsDb");
            MaxTimeoutMsec = int.Parse(config["Kafka:MaxTimeoutMsec"]);
            MaxThreadsCount = int.Parse(config["Kafka:MaxThreadsCount"]);

            Config = config.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            ApplyKafkaAliases(config, Config);

            CriticalErrorsCount = int.Parse(config["Health:CriticalErrorsCount"]);
            ForPeriodInSec = int.Parse(config["Health:ForPeriodInSec"]);
        }

        private static void ApplyKafkaAliases(IConfiguration configuration, Dictionary<string, string> Config)
        {
            var aliases = configuration.GetSection("Kafka:Aliases").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            foreach (var item in aliases)
            {
                var value = configuration[$"Kafka:{item.Key}"];

                if (!string.IsNullOrEmpty(value))
                {
                    Config[item.Value] = value;
                }
            }
        }
    }
}
