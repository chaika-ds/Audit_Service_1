using System.Diagnostics;
using AuditService.Common.Models.Dto;
using AuditService.Handlers.Consts;
using KIT.Kafka.HealthCheck;
using KIT.Redis.HealthCheck;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Nest;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Service health check request handler
/// </summary>
public class HealthCheckRequestHandler : IRequestHandler<CheckElkHealthRequest, bool>,
    IRequestHandler<CheckKafkaHealthRequest, bool>, IRequestHandler<CheckRedisHealthRequest, bool>,
    IRequestHandler<CheckHealthRequest, HealthCheckResponseDto>
{
    private readonly IElasticClient _elasticClient;
    private readonly IKafkaHealthCheck _kafkaHealthCheck;
    private readonly IRedisHealthCheck _redisHealthCheck;

    public HealthCheckRequestHandler(IElasticClient elasticClient, IKafkaHealthCheck kafkaHealthCheck,
        IRedisHealthCheck redisHealthCheck)
    {
        _elasticClient = elasticClient;
        _kafkaHealthCheck = kafkaHealthCheck;
        _redisHealthCheck = redisHealthCheck;
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

    /// <summary>
    ///     Handle a request for a health check of the Redis service
    /// </summary>
    /// <param name="request">Redis service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    public async Task<bool> Handle(CheckRedisHealthRequest request, CancellationToken cancellationToken) =>
        (await _redisHealthCheck.CheckHealthAsync(cancellationToken)).Status == HealthStatus.Healthy;

    /// <summary>
    ///     Handle a request for a health check of the all services
    /// </summary>
    /// <param name="request">All service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    public async Task<HealthCheckResponseDto> Handle(CheckHealthRequest request, CancellationToken cancellationToken)
    {
        var response = new HealthCheckResponseDto();

        response.Components.Add(HealthCheckConst.Kafka, await GetComponentAsync(nameof(HealthCheckConst.Kafka), cancellationToken));
        response.Components.Add(HealthCheckConst.Elk, await GetComponentAsync(nameof(HealthCheckConst.Elk), cancellationToken));
        response.Components.Add(HealthCheckConst.Redis, await GetComponentAsync(nameof(HealthCheckConst.Redis), cancellationToken));

        response.Version = GetVersionDto();

        return response;
    }

    /// <summary>
    ///     Creates a Component and tracks time for the selected services
    /// </summary>
    /// <param name="name">Selected service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Components Dto with selected service</returns>
    private async Task<ComponentsDto> GetComponentAsync(string name, CancellationToken cancellationToken)
    {
        var stopwatch = new Stopwatch();

        stopwatch.Start();

        var status = await GetServiceStatusAsync(name, cancellationToken);

        stopwatch.Stop();

        return new ComponentsDto()
        {
            Name = name,
            RequestTime = stopwatch.ElapsedMilliseconds,
            Status = status
        };
    }

    /// <summary>
    ///     Handle a request for a health check selected services
    /// </summary>
    /// <param name="name">Selected service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    private async Task<bool> GetServiceStatusAsync(string name, CancellationToken cancellationToken)
    {
        return @name switch
        {
            HealthCheckConst.Kafka => await Handle(new CheckKafkaHealthRequest(), cancellationToken),
            HealthCheckConst.Elk => await Handle(new CheckElkHealthRequest(), cancellationToken),
            HealthCheckConst.Redis => await Handle(new CheckRedisHealthRequest(), cancellationToken),
            _ => false
        };
    }


    /// <summary>
    ///     Creates a Version Dto
    /// </summary>
    /// <returns>Version Dto of current branch</returns>
    private VersionDto GetVersionDto()
    {
        var startInfo = new ProcessStartInfo("git")
        {
            WorkingDirectory = Directory.GetParent(Directory.GetCurrentDirectory())?.Parent?.FullName,
            UseShellExecute = false,
            RedirectStandardInput = true,
            RedirectStandardOutput = true
        };

        var branch = GitInfoByArgument("rev-parse --abbrev-ref HEAD", startInfo);
        var lastCommit = GitInfoByArgument("rev-parse --verify HEAD", startInfo);
        var tag = GitInfoByArgument("describe --tags --exact-match", startInfo);

        return new VersionDto
        {
            Branch = branch,
            Commit = lastCommit,
            Tag = tag
        };
    }

    /// <summary>
    ///     Creates a Version Dto
    /// </summary>
    /// <param name="argument">Git command as argument</param>
    /// <param name="startInfo">SProcessStartInfo</param>
    /// <returns>Result of git command</returns>
    private string? GitInfoByArgument(string argument, ProcessStartInfo startInfo)
    {
        startInfo.Arguments = argument;

        using var process = new Process {StartInfo = startInfo};
        
        process.Start();

        return process.StandardOutput.ReadLine();
    }
}