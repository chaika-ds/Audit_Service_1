using AuditService.Kafka.Settings;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using Tolar.Authenticate.Impl;
using Tolar.Kafka;

namespace AuditService.EventConsumerApp
{
    /// <summary>
    /// Application settings for Kafka consumer
    /// </summary>
    public class AppSettings : IKafkaConsumerSettings, IHealthSettings, IAuthenticateServiceSettings
    {

        /// <summary>
        ///     Application settings
        /// </summary>
        public AppSettings(IConfiguration config)
        {
            ApplySsoSection(config);
            ApplyKafkaSection(config);
            ApplyHealthSection(config);
        }

        #region Health

        public int CriticalErrorsCount { get; set; }
        public int ForPeriodInSec { get; set; }

        /// <summary>
        ///     Apply Health configs
        /// </summary>
        private void ApplyHealthSection(IConfiguration config)
        {
            CriticalErrorsCount = int.Parse(config["Kafka:HealthCheck:CriticalErrorsCount"]);
            ForPeriodInSec = int.Parse(config["Kafka:HealthCheck:ForPeriodInSecond"]);
        }

        #endregion

        #region Kafka

        public int MaxTimeoutMsec { get; set; }
        public int MaxThreadsCount { get; set; }

        public Dictionary<string, string> Config { get; set; }

        /// <summary>
        ///     Apply Kafka configs
        /// </summary>
        private void ApplyKafkaSection(IConfiguration config)
        {
            MaxTimeoutMsec = int.Parse(config["Kafka:MaxTimeoutMsec"]);
            MaxThreadsCount = int.Parse(config["Kafka:MaxThreadsCount"]);

            Config = config.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            ApplyKafkaAliases(config, Config);
        }

        private void ApplyKafkaAliases(IConfiguration configuration, IDictionary<string, string> config)
        {
            var aliases = configuration.GetSection("Kafka:Aliases").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            foreach (var item in aliases)
            {
                var value = configuration[$"Kafka:{item.Key}"];

                if (!string.IsNullOrEmpty(value)) config[item.Value] = value;
            }
        }

        #endregion

        #region SSO

        /// <summary>
        ///     Link to SSO
        /// </summary>
        public string Connection { get; private set; }

        /// <summary>
        ///     Service ID
        /// </summary>
        public Guid ServiceId { get; private set; }

        /// <summary>
        ///     API Secret Key
        /// </summary>
        public string ApiKey { get; private set; }

        /// <summary>
        ///     Root id from structure of casino
        /// </summary>
        public Guid RootNodeId { get; private set; }

        /// <summary>
        ///     Apply SSO configs
        /// </summary>
        private void ApplySsoSection(IConfiguration config)
        {
            Connection = config["SSO:Url"];
            ServiceId = Guid.Parse(config["SSO:ServiceId"] ?? throw new InvalidOperationException("Wrong ServiceId."));
            ApiKey = config["SSO:ApiKey"];
            RootNodeId = Guid.Parse(config["SSO:RootNodeId"] ?? throw new InvalidOperationException("Wrong RootNodeId."));
        }

        #endregion        
    }
}