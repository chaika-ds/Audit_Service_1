using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace KIT.Redis.HealthCheck;

/// <summary>
///     Service to check the health of Redis
/// </summary>
public interface IRedisHealthCheck
{
    /// <summary>
    ///     Check the health of the Redis service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Represents the result of a health check</returns>
    Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = default);
}