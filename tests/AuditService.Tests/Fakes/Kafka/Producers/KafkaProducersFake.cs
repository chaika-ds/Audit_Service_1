using Tolar.Kafka;

namespace AuditService.Tests.Fakes.Kafka.Producers;

/// <summary>
/// Fake KafkaProducer 
/// </summary>
public class KafkaProducersFake : IKafkaProducer
{
    /// <summary>
    /// Check is SendAsync executed 
    /// </summary>
    public static bool IsSendAsyncExecuted;
   
    /// <summary>
    /// Fake SendAsync Method
    /// </summary>
    public Task SendAsync<T>(T obj, string topic, object key = null)
    {
        IsSendAsyncExecuted = true;
        
        return Task.CompletedTask;
    }
}