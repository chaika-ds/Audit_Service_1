using System.Net;
using AuditService.Common.Extensions;
using KIT.NLog.Extensions;
using KIT.Redis.Consts;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Tolar.Redis;

namespace KIT.Redis.HealthCheck;

/// <summary>
///     Service to check the health of Redis
/// </summary>
public class RedisHealthCheck : IRedisHealthCheck, IDisposable
{
    private readonly ILogger<RedisHealthCheck> _logger;
    private readonly IConnectionMultiplexer _connection;

    public RedisHealthCheck(IRedisSettings redisSettings, ILogger<RedisHealthCheck> logger)
    {
        _logger = logger;
        _connection = ConnectionMultiplexer.Connect(redisSettings.RedisConnectionString);
    }

    /// <summary>
    ///     Check the health of the Redis service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Represents the result of a health check</returns>
    public async Task<HealthCheckResult> CheckHealthAsync(CancellationToken cancellationToken = default)
    {
        var message = $"Check Redis healthy on {DateTime.UtcNow}";
        try
        {
            foreach (var endPoint in _connection.GetEndPoints(true))
            {
                var result = await CheckHealthForEndPoint(endPoint);

                if (result.Status == HealthStatus.Healthy)
                    continue;

                _logger.LogError(result.Description!, message);
                return result;
            }

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, message);
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    }

    /// <summary>
    /// Check server health for endpoint
    /// </summary>
    /// <param name="endPoint">Endpoint of server</param>
    /// <returns>Represents the result of a health check</returns>
    private async Task<HealthCheckResult> CheckHealthForEndPoint(EndPoint? endPoint)
    {
        var server = _connection.GetServer(endPoint);

        if (server.ServerType != ServerType.Cluster)
            return await CheckNonClusterServerHealth(server);

        return await CheckClusterServerHealth(server);
    }

    /// <summary>
    /// Check the health of a clustered server
    /// </summary>
    /// <param name="server">Redis server</param>
    /// <returns>Represents the result of a health check</returns>
    private static async Task<HealthCheckResult> CheckClusterServerHealth(IServer server)
    {
        var clusterInfo = await server.ExecuteAsync(RedisClusterPropsConst.Cluster, RedisClusterPropsConst.InfoCommand);

        if (clusterInfo is null || clusterInfo.IsNull)
            return HealthCheckResult.Unhealthy("Redis cluster is null or can't be read");

        return !clusterInfo.ToString()!.Contains(RedisClusterPropsConst.ClusterStateOk)
            ? HealthCheckResult.Degraded("Redis cluster is not on OK state")
            : HealthCheckResult.Healthy();
    }

    /// <summary>
    /// Check the health of a non-clustered server
    /// </summary>
    /// <param name="server">Redis server</param>
    /// <returns>Represents the result of a health check</returns>
    private async Task<HealthCheckResult> CheckNonClusterServerHealth(IRedisAsync server)
    {
        try
        {
            await _connection.GetDatabase().PingAsync();
            await server.PingAsync();

            return HealthCheckResult.Healthy();
        }
        catch (Exception ex)
        {
            return HealthCheckResult.Unhealthy(ex.FullMessage());
        }
    }

    public void Dispose()
    {
        _connection.Close();
        _connection.Dispose();
    }
}