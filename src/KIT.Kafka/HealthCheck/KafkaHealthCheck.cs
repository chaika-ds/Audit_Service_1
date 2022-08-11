using System.Diagnostics;
using AuditService.Common.Consts;
using AuditService.Common.Models.Dto;
using KIT.Kafka.Settings.Interfaces;
using KIT.NLog.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Tolar.Kafka;

namespace KIT.Kafka.HealthCheck;

/// <summary>
///     Service to check the health of Kafka
/// </summary>
public class KafkaHealthCheck : IKafkaHealthCheck
{
    private readonly IKafkaProducer _kafkaProducer;
    private readonly IKafkaTopics _kafkaTopics;
    private readonly ILogger<KafkaHealthCheck> _logger;

    public KafkaHealthCheck(IKafkaProducer kafkaProducer, IKafkaTopics kafkaTopics,
        ILogger<KafkaHealthCheck> logger)
    {
        _kafkaProducer = kafkaProducer;
        _kafkaTopics = kafkaTopics;
        _logger = logger;
    }

    /// <summary>
    ///     Check the health of the kafka service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Represents the result of a health check</returns>
    public async Task<HealthCheckComponentsDto> CheckHealthAsync(CancellationToken cancellationToken = default)
    {
        var message = $"Check Kafka healthy on {DateTime.UtcNow}";
        
        var stopwatch = new Stopwatch();
        
        try
        {
            stopwatch.Start();
            
            var task = _kafkaProducer.SendAsync(message, _kafkaTopics.HealthCheck);

            if (await Task.WhenAny(task, Task.Delay(TimeSpan.FromSeconds(1), cancellationToken)) == task)
            {
                stopwatch.Stop();

                return new HealthCheckComponentsDto()
                {
                    Name = nameof(HealthCheckConst.Kafka),
                    RequestTime = stopwatch.ElapsedMilliseconds,
                    Status = HealthCheckResult.Healthy().Status == HealthStatus.Healthy
                };
            }

            stopwatch.Stop();
            
            var errorMessage = "The kafka service is not responding";
            
            _logger.LogError(errorMessage, message);
            
            return new HealthCheckComponentsDto()
            {
                Name = nameof(HealthCheckConst.Kafka),
                RequestTime = stopwatch.ElapsedMilliseconds,
                Status =HealthCheckResult.Degraded(errorMessage).Status == HealthStatus.Healthy
            };
        }
        catch (Exception ex)
        {
            stopwatch.Stop();

            _logger.LogException(ex, "Kafka service health check failed", message);
            
            return new HealthCheckComponentsDto()
            {
                Name = nameof(HealthCheckConst.Kafka),
                RequestTime = stopwatch.ElapsedMilliseconds,
                Status = HealthCheckResult.Unhealthy(ex.Message, ex).Status == HealthStatus.Healthy
            };
        }
    }
}