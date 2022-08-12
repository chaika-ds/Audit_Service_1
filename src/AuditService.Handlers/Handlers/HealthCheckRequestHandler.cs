using System.Diagnostics;
using AuditService.Common.Consts;
using AuditService.Common.Models.Dto;
using AuditService.Setup.AppSettings;
using KIT.Kafka.HealthCheck;
using KIT.Redis.HealthCheck;
using MediatR;
using Nest;
using GitLabApiClient;


namespace AuditService.Handlers.Handlers;

/// <summary>
///     Service health check request handler
/// </summary>
public class HealthCheckRequestHandler : IRequestHandler<CheckHealthRequest, HealthCheckResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IElasticClient _elasticClient;
    private readonly IKafkaHealthCheck _kafkaHealthCheck;
    private readonly IRedisHealthCheck _redisHealthCheck;


    public HealthCheckRequestHandler(IMediator mediator, IElasticClient elasticClient, IKafkaHealthCheck kafkaHealthCheck,
        IRedisHealthCheck redisHealthCheck)
    {
        _elasticClient = elasticClient;
        _kafkaHealthCheck = kafkaHealthCheck;
        _redisHealthCheck = redisHealthCheck;
        _mediator = mediator;
    }

    /// <summary>
    ///     Handle a request for a health check of the all services
    /// </summary>
    /// <param name="request">All service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    public async Task<HealthCheckResponseDto> Handle(CheckHealthRequest request, CancellationToken cancellationToken)
    {
        var response = new HealthCheckResponseDto();

        response.Components.Add(HealthCheckConst.Kafka, await _kafkaHealthCheck.CheckHealthAsync(cancellationToken));
        response.Components.Add(HealthCheckConst.Redis, await _redisHealthCheck.CheckHealthAsync(cancellationToken));
        response.Components.Add(HealthCheckConst.Elk, await CheckElkHealthAsync(cancellationToken));

        response.GitLabVersionResponse = await _mediator.Send(new GitLabRequest(), cancellationToken);

        return response;
    }

    /// <summary>
    ///     Handle a request for a health check of the Elk service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    private async Task<HealthCheckComponentsDto> CheckElkHealthAsync(CancellationToken cancellationToken)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        var status = (await _elasticClient.Cluster.HealthAsync(ct: cancellationToken)).ApiCall.Success;

        stopwatch.Stop();

        return new HealthCheckComponentsDto()
        {
            Name = HealthCheckConst.Elk,
            RequestTime = stopwatch.ElapsedMilliseconds,
            Status = status
        };
    }
}