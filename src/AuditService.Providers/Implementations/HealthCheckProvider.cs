using AuditService.Kafka.AppSetings;
using AuditService.Kafka.Services.Health;
using AuditService.Providers.Interfaces;
using Nest;

namespace AuditService.Providers.Implementations;

/// <summary>
///     Health check
/// </summary>
public class HealthCheckProvider : IHealthCheckProvider
{
    private readonly IElasticClient _elasticClient;
    private readonly IHealthService _healthService;
    private readonly IHealthSettings _settings;

    /// <summary>
    ///     Health check
    /// </summary>
    public HealthCheckProvider(IHealthSettings settings, IHealthService healthService, IElasticClient elasticClient)
    {
        _settings = settings;
        _healthService = healthService;
        _elasticClient = elasticClient;
    }

    /// <summary>
    ///     Check ElasticSearch
    /// </summary>
    public bool CheckElkHealth()
    {
        var elkResponse = _elasticClient.Cluster.Health();
        return elkResponse.ApiCall.Success;
    }

    /// <summary>
    ///     Check Kafka
    /// </summary>
    public bool CheckKafkaHealth()
    {
        return _healthService.GetErrorsCount() < _settings.CriticalErrorsCount;
    }
}