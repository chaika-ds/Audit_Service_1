using AuditService.Kafka.AppSetings;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Tolar.Kafka;

namespace AuditService.Kafka.Kafka
{
    /// <summary>
    /// Producer for messages to Kafka
    /// </summary>
    public sealed class KafkaProducer : IKafkaProducer, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IKafkaSettings _settings;

        private readonly string _sessionId;
        private readonly IProducer<string, string> _producer;
        private readonly JsonSerializerSettings _serializerSettings;

        private bool _disposed;

        public KafkaProducer(ILogger<KafkaProducer> logger, IKafkaSettings settings)
        {
            _logger = logger;
            _settings = settings;

            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                Formatting = Formatting.None,
            };

            _sessionId = $"{Environment.MachineName}/{DateTime.UtcNow:o}";

            _producer = new ProducerBuilder<string, string>(_settings.Config)
                .SetKeySerializer(Serializers.Utf8)
                .SetValueSerializer(Serializers.Utf8)
                .SetLogHandler(LogHandler)
                .SetErrorHandler(ErrorHandler)
                .Build();

            _disposed = false;
        }

        public async Task SendAsync<T>(T obj, string topic, object? key = null)
        {
            await SendAsync(obj, topic);
        }

        public async Task SendAsync<T>(T obj, string topic)
        {
            if (obj == null)
            {
                throw new KafkaProducerException("Notification is null");
            }

            var objStr = JsonConvert.SerializeObject(obj, _serializerSettings);

            try
            {
                var msg = new Message<string, string>();
                msg.Key = JsonConvert.SerializeObject(
                 new Key
                 {
                     Type = nameof(T),
                     SessionId = _sessionId,
                 }, _serializerSettings);

                msg.Value = objStr;
                var dr = await _producer.ProduceAsync(topic, msg);

                if (dr.Status == PersistenceStatus.NotPersisted)
                {
                    throw new KafkaProducerException("Message wasn't transmit");
                }
            }
            catch (KafkaException ex)
            {
                throw new KafkaProducerException("Error in kafka on send message: " + ex.Message);
            }
        }

        public void Dispose() => Dispose(true);

        private void LogHandler(IProducer<string, string> producer, LogMessage log)
        {
            _logger.LogInformation("{level} {name}: {message}. Session: {session}", log.Level, log.Name, log.Message, _sessionId);
        }

        private void ErrorHandler(IProducer<string, string> producer, Error error)
        {
            if (error.IsFatal)
            {
                _logger?.LogCritical("[{code}] IsBroker: {isBrocker}. {reason}", error.Code, error.IsBrokerError, error.Reason);
            }
            else
            {
                _logger?.LogError("[{code}] IsBroker: {isBrocker}. {reason}", error.Code, error.IsBrokerError, error.Reason);
            }
        }

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _producer?.Dispose();
            }

            _disposed = true;
        }
    }
}
