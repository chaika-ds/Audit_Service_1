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
    private readonly IElasticClient _elasticClient;
    private readonly IKafkaHealthCheck _kafkaHealthCheck;
    private readonly IRedisHealthCheck _redisHealthCheck;
    private readonly IGitlabSettings _gitlabSettings;
    private readonly IGitLabClient _gitLabClient;


    public HealthCheckRequestHandler(IElasticClient elasticClient, IKafkaHealthCheck kafkaHealthCheck,
        IRedisHealthCheck redisHealthCheck, IGitLabClient gitLabClient, IGitlabSettings gitlabSettings)
    {
        _elasticClient = elasticClient;
        _kafkaHealthCheck = kafkaHealthCheck;
        _redisHealthCheck = redisHealthCheck;
        _gitLabClient = gitLabClient;
        _gitlabSettings = gitlabSettings;
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

        response.HealthCheckVersion = await GetVersionDtoAsync();

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
            Name = nameof(HealthCheckConst.Elk),
            RequestTime = stopwatch.ElapsedMilliseconds,
            Status = status
        };
    }


    /// <summary>
    ///     Creates a Version Dto
    /// </summary>
    /// <returns>Version Dto of current branch</returns>
    private async Task<HealthCheckVersionDto> GetVersionDtoAsync()
    {
        var branchInfo = await _gitLabClient.Branches.GetAsync(_gitlabSettings.ProjectId, _gitlabSettings.BranchName);

        var tags = await _gitLabClient.Tags.GetAsync(_gitlabSettings.ProjectId);

        return new HealthCheckVersionDto
        {
            Branch = branchInfo.Name,
            Commit = branchInfo.Commit.Id,
            Tag = tags.MaxBy(x => x.Commit.CreatedAt)?.Name
        };
    }
}