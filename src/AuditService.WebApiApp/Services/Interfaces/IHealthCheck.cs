namespace AuditService.WebApiApp.Services.Interfaces;

/// <summary>
///     Health check
/// </summary>
public interface IHealthCheck
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