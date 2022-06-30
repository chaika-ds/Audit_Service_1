using Microsoft.Extensions.Configuration;
using Tolar.Kafka;

namespace AuditService.Kafka.AppSetings
{
    internal class KafkaConsumerSettings : IKafkaConsumerSettings
    {
        public KafkaConsumerSettings(IConfiguration configuration) => ApplyKafkaSection(configuration);

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

            Config = config.GetSection("Kafka:ConsumerConfig").GetChildren().ToDictionary(x => x.Key, v => v.Value);

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
    }
}
