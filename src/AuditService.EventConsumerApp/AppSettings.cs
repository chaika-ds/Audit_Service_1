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
            CriticalErrorsCount = int.Parse(config["KAFKA:HEALTH_CHECK:CRITICAL_ERRORS_COUNT"]);
            ForPeriodInSec = int.Parse(config["KAFKA:HEALTH_CHECK:FOR_PERIOD_IN_SEC"]);
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
            MaxTimeoutMsec = int.Parse(config["KAFKA:MaxTimeoutMsec"]);
            MaxThreadsCount = int.Parse(config["KAFKA:MaxThreadsCount"]);

            Config = config.GetSection("KAFKA:CONFIGS").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            ApplyKafkaAliases(config, Config);
        }

        private void ApplyKafkaAliases(IConfiguration configuration, IDictionary<string, string> config)
        {
            var aliases = configuration.GetSection("KAFKA:Aliases").GetChildren().ToDictionary(x => x.Key, v => v.Value);

            foreach (var item in aliases)
            {
                var value = configuration[$"KAFKA:{item.Key}"];

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
            Connection = config["SSO:SSO_SERVICE_URL"];
            ServiceId = Guid.Parse(config["SSO:SSO_AUTH_SERVICE_ID"] ?? throw new InvalidOperationException("Wrong ServiceId."));
            ApiKey = config["SSO:SSO_AUTH_API_KEY"];
            RootNodeId = Guid.Parse(config["SSO:SSO_AUTH_ROOT_NODE_ID"] ?? throw new InvalidOperationException("Wrong RootNodeId."));
        }

        #endregion        
    }
}