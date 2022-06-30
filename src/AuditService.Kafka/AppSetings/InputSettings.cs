using AuditService.Kafka.Services;
using Microsoft.Extensions.Configuration;

namespace AuditService.Kafka.AppSetings
{
    public class InputSettings<T> : IInputSettings<T>
    {
        private const string KAFKA_INPUT_SECTION = "KAFKA:KAFKA_TOPICS";

        public InputSettings(IConfiguration config)
        {
            Topic = config[$"{KAFKA_INPUT_SECTION}:{typeof(T).Name}"];
        }

        public string Name { get; set; }

        public string Topic { get; set; }
    }
}
