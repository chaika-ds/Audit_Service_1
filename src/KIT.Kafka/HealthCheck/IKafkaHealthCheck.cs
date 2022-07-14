using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KIT.Kafka.HealthCheck;

/// <summary>
///     Service to check the health of Kafka
/// </summary>
public interface IKafkaHealthCheck
{
    /// <summary>
    ///     Check the health of the kafka service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Represents the result of a health check</returns>
    Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = default);
}