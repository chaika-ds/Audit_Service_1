using AuditService.Common.ELK;
using AuditService.Common.Health;
using AuditService.WebApiApp.Services.Interfaces;
using Elasticsearch.Net;
using Microsoft.Extensions.Options;
using Nest;

namespace AuditService.WebApiApp.Services;

public class HealthCheckService :IHealthCheck
{
    private readonly ElasticOptions _elasticSearchConfiguration;
    private readonly IHealthService _healthService;
    private readonly IHealthSettings _settings;

    public HealthCheckService(IHealthSettings settings, IHealthService healthService, IOptions<ElasticOptions> elasticSearchConfiguration)
    {
        _settings = settings;
        _healthService = healthService;
        _elasticSearchConfiguration = elasticSearchConfiguration.Value;
    }

    public bool CheckElkHealth()
    {
        var nodes = _elasticSearchConfiguration.Uris;
        var pool = new StaticConnectionPool(nodes);
        var settings = new ConnectionSettings(pool);
        var client = new ElasticClient(settings);
        
        var elkResponse =  client.Cluster.Health();

        return elkResponse.ApiCall.Success;
    }

    public bool CheckKafkaHealth()
    {
        var t = _healthService.GetErrorsCount();
        return _healthService.GetErrorsCount() < _settings.CriticalErrorsCount;
    }
}