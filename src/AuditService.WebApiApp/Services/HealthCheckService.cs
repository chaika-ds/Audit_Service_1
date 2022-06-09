using AuditService.Common.Health;
using AuditService.WebApiApp.Services.Interfaces;
using Nest;

namespace AuditService.WebApiApp.Services;

/// <summary>
///     Health check
/// </summary>
internal class HealthCheckService : IHealthCheck
{
    private readonly IElasticClient _elasticClient;
    private readonly IHealthService _healthService;
    private readonly IHealthSettings _settings;

    /// <summary>
    ///     Health check
    /// </summary>
    public HealthCheckService(IHealthSettings settings, IHealthService healthService, IElasticClient elasticClient)
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
        var t = _healthService.GetErrorsCount();
        return _healthService.GetErrorsCount() < _settings.CriticalErrorsCount;
    }
}