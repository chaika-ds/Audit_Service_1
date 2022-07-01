namespace AuditService.Providers.Interfaces;

/// <summary>
///     Health check
/// </summary>
public interface IHealthCheckProvider
{
    /// <summary>
    ///     Check ElasticSearch
    /// </summary>
    Task<bool> CheckElkHealthAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Check Kafka
    /// </summary>
    bool CheckKafkaHealth();
}