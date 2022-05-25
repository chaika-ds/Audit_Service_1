using AuditService.Common.Args;
using Microsoft.VisualStudio.Threading;

namespace AuditService.Common.Kafka
{
    public interface IKafkaConsumer
    {
        event AsyncEventHandler<MessageReceivedArgumentEventArgs> MessageReceived;

        event EventHandler ConsumerInitialized;

        event EventHandler KafkaError;

        void Start();

        void StartWithRollback(int rollbackInMinutes);

        void Stop();
    }
}
