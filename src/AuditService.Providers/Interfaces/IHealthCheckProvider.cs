namespace AuditService.Providers.Interfaces;

/// <summary>
///     Health check
/// </summary>
public interface IHealthCheckProvider
{
    /// <summary>
    ///     Check ElasticSearch
    /// </summary>
    bool CheckElkHealth();

    /// <summary>
    ///     Check Kafka
    /// </summary>
    bool CheckKafkaHealth();
}