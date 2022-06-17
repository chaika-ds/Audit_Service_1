using System.Collections.Generic;
using System.Linq;
using AuditService.IntegrationTests.EventProducer.Builder;
using Microsoft.Extensions.Configuration;
using AuditService.Kafka.Settings;

namespace AuditService.IntegrationTests.EventProducer.Settings
{
    public class AppSettings : IKafkaSettings, IDirectorSettings
    {
        public string? GroupId { get; set; }
        public string? Address { get; set; }
        public string? Topic { get; set; }

        public Dictionary<string, string>? Config { get; set; }
        public Dictionary<string, string> Topics { get; set; }

        public AppSettings()
        {
            AddTestConfiguration();
        }

        private void AddTestConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", optional: false);
            var configuration = builder.Build();

            GroupId = configuration["Kafka:GroupId"];
            Address = configuration["Kafka:Address"];
            Config = configuration.GetSection("Kafka:Config").GetChildren().ToDictionary(x => x.Key, v => v.Value);
            Topics = configuration.GetSection("KafkaTopics").GetChildren().ToDictionary(x => x.Key, v => v.Value);
        }
    }
}
