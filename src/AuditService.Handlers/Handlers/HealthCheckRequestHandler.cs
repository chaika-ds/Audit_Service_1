using System.Diagnostics;
using AuditService.Common.Models.Dto;
using AuditService.Handlers.Consts;
using AuditService.Setup.AppSettings;
using KIT.Kafka.HealthCheck;
using KIT.Redis.HealthCheck;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
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
        IRedisHealthCheck redisHealthCheck, IGitlabSettings gitlabSettings)
    {
        _elasticClient = elasticClient;
        _kafkaHealthCheck = kafkaHealthCheck;
        _redisHealthCheck = redisHealthCheck;
        _gitlabSettings = gitlabSettings;

        _gitLabClient = new GitLabClient(gitlabSettings.Url);
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

        response.Components.Add(HealthCheckConst.Kafka, await CheckKafkaHealthAsync(cancellationToken));
        response.Components.Add(HealthCheckConst.Elk, await CheckElkHealthAsync(cancellationToken));
        response.Components.Add(HealthCheckConst.Redis, await CheckRedisHealthAsync(cancellationToken));

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
    ///     Handle a request for a health check of the Kafka service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    private async Task<HealthCheckComponentsDto> CheckKafkaHealthAsync(CancellationToken cancellationToken)
    {
        var healthCheckDto = await _kafkaHealthCheck.CheckHealthAsync(cancellationToken);

        return new HealthCheckComponentsDto()
        {
            Name = nameof(HealthCheckConst.Kafka),
            RequestTime = healthCheckDto.ElapsedMilliseconds,
            Status = healthCheckDto.HealthCheckResult.Status == HealthStatus.Healthy
        };
    }


    /// <summary>
    ///     Handle a request for a health check of the Redis service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    private async Task<HealthCheckComponentsDto> CheckRedisHealthAsync(CancellationToken cancellationToken)
    {
        var healthCheckDto = await _redisHealthCheck.CheckHealthAsync(cancellationToken);

        return new HealthCheckComponentsDto
        {
            Name = nameof(HealthCheckConst.Redis),
            RequestTime = healthCheckDto.ElapsedMilliseconds,
            Status = healthCheckDto.HealthCheckResult.Status == HealthStatus.Healthy
        };
    }


    /// <summary>
    ///     Creates a Version Dto
    /// </summary>
    /// <returns>Version Dto of current branch</returns>
    private async Task<HealthCheckVersionDto> GetVersionDtoAsync()
    {
        await _gitLabClient.LoginAsync(_gitlabSettings.Username, _gitlabSettings.Password);

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