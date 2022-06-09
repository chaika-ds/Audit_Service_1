using Tolar.Authenticate;
using System.Threading.Tasks;
using Tolar.Kafka;

namespace AuditService.EventConsumerApp.Auth
{
    public class PermissionPusherImpl : PermissionPusher
    {
        private readonly IKafkaProducer _producer;
        private readonly IPermissionPusherSettings _settings;

        public PermissionPusherImpl(IKafkaProducer producer, IPermissionPusherSettings settings)
            : base(settings.ServiceId, settings.ServiceName)
        {
            _producer = producer;
            _settings = settings;
        }

        protected override Task PushAsync(object obj)
        {
            return _producer.SendAsync(obj, _settings.Topic);
        }
    }
}
