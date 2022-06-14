using Microsoft.Extensions.Configuration;

namespace AuditService.Kafka.Services.ExternalConnectionServices
{
    public class InputSettings<T> : IInputSettings<T>
    {
        private const string KAFKA_INPUT_SECTION = "Kafka:InputSettings";

        public InputSettings(IConfiguration config)
        {
            Topic = config[$"{KAFKA_INPUT_SECTION}:{typeof(T).Name}"];
        }

        public string Name { get; set; }

        public string Topic { get; set; }
    }
}
