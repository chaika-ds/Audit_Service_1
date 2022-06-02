using AuditService.Common.Health;
using AuditService.WebApiApp.Services.Interfaces;
using Elasticsearch.Net;
using Nest;

namespace AuditService.WebApiApp.Services;

public class HealthCheckService :IHealthCheck
{
    private readonly IHealthService _healthService;
    private readonly IElasticClient _elasticClient;
    private readonly IHealthSettings _settings;

    public HealthCheckService(IHealthSettings settings, IHealthService healthService, IElasticClient elasticClient)
    {
        _settings = settings;
        _healthService = healthService;
        _elasticClient = elasticClient;
    }

    public bool CheckElkHealth()
    {
        var elkResponse = _elasticClient.Cluster.Health();
        return elkResponse.ApiCall.Success;
    }

    public bool CheckKafkaHealth()
    {
        var t = _healthService.GetErrorsCount();
        return _healthService.GetErrorsCount() < _settings.CriticalErrorsCount;
    }
}