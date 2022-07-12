using AuditService.Common.Models.Dto;
using KIT.Kafka.HealthCheck;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Nest;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Service health check request handler
/// </summary>
public class HealthCheckRequestHandler : IRequestHandler<CheckElkHealthRequest, bool>,
    IRequestHandler<CheckKafkaHealthRequest, bool>
{
    private readonly IElasticClient _elasticClient;
    private readonly IKafkaHealthCheck _kafkaHealthCheck;

    public HealthCheckRequestHandler(IElasticClient elasticClient, IKafkaHealthCheck kafkaHealthCheck)
    {
        _elasticClient = elasticClient;
        _kafkaHealthCheck = kafkaHealthCheck;
    }

    /// <summary>
    ///     Handle a request for a health check of the Elk service
    /// </summary>
    /// <param name="request">Elk service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    public async Task<bool> Handle(CheckElkHealthRequest request, CancellationToken cancellationToken) =>
        (await _elasticClient.Cluster.HealthAsync(ct: cancellationToken)).ApiCall.Success;

    /// <summary>
    ///     Handle a request for a health check of the Kafka service
    /// </summary>
    /// <param name="request">Kafka service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    public async Task<bool> Handle(CheckKafkaHealthRequest request, CancellationToken cancellationToken) =>
        (await _kafkaHealthCheck.CheckHealthAsync(cancellationToken)).Status == HealthStatus.Healthy;
}