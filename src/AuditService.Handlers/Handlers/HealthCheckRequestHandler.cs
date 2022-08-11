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
public class HealthCheckRequestHandler :  IRequestHandler<CheckHealthRequest, HealthCheckResponseDto>
{
    private readonly IElasticClient _elasticClient;
    private readonly IKafkaHealthCheck _kafkaHealthCheck;
    private readonly IRedisHealthCheck _redisHealthCheck;
    private readonly Stopwatch _stopwatch;


    public HealthCheckRequestHandler(IElasticClient elasticClient, IKafkaHealthCheck kafkaHealthCheck,
        IRedisHealthCheck redisHealthCheck)
    {
        _elasticClient = elasticClient;
        _kafkaHealthCheck = kafkaHealthCheck;
        _redisHealthCheck = redisHealthCheck;
        _stopwatch = new Stopwatch();
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

        response.Components.Add(HealthCheckConst.Kafka, await CheckKafkaHealthAsync(nameof(HealthCheckConst.Kafka), cancellationToken));
        response.Components.Add(HealthCheckConst.Elk, await CheckElkHealthAsync(nameof(HealthCheckConst.Elk), cancellationToken));
        response.Components.Add(HealthCheckConst.Redis, await CheckRedisHealthAsync(nameof(HealthCheckConst.Redis), cancellationToken));

        response.HealthCheckVersion = GetVersionDto();

        return response;
    }
    
    /// <summary>
    ///     Handle a request for a health check of the Elk service
    /// </summary>
    /// <param name="name">Selected service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    private async Task<HealthCheckComponentsDto> CheckElkHealthAsync(string name, CancellationToken cancellationToken)
    {
        _stopwatch.Reset();
        _stopwatch.Start();
        
        var status =  (await _elasticClient.Cluster.HealthAsync(ct: cancellationToken)).ApiCall.Success;

        _stopwatch.Stop();

        return new HealthCheckComponentsDto()
        {
            Name = name,
            RequestTime = _stopwatch.ElapsedMilliseconds,
            Status = status
        };
    }

    
    /// <summary>
    ///     Handle a request for a health check of the Kafka service
    /// </summary>
    /// <param name="name">Selected service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    private async Task<HealthCheckComponentsDto> CheckKafkaHealthAsync(string name, CancellationToken cancellationToken)
    {
        _stopwatch.Reset();
        _stopwatch.Start();
        
        var status =   (await _kafkaHealthCheck.CheckHealthAsync(cancellationToken)).Status == HealthStatus.Healthy;

        _stopwatch.Stop();

        return new HealthCheckComponentsDto()
        {
            Name = name,
            RequestTime = _stopwatch.ElapsedMilliseconds,
            Status = status
        };
    }
       

    /// <summary>
    ///     Handle a request for a health check of the Redis service
    /// </summary>
    /// <param name="name">Selected service health check request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Service health check result</returns>
    private async Task<HealthCheckComponentsDto> CheckRedisHealthAsync(string name, CancellationToken cancellationToken)
    {
        _stopwatch.Reset();
        _stopwatch.Start();
        
        var status =   (await _redisHealthCheck.CheckHealthAsync(cancellationToken)).Status == HealthStatus.Healthy;

        _stopwatch.Stop();

        return new HealthCheckComponentsDto()
        {
            Name = name,
            RequestTime = _stopwatch.ElapsedMilliseconds,
            Status = status
        };
    }


    /// <summary>
    ///     Creates a Version Dto
    /// </summary>
    /// <returns>Version Dto of current branch</returns>
    private HealthCheckVersionDto GetVersionDto()
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
        var tag = GitInfoByArgument("describe --tags", startInfo);

        return new HealthCheckVersionDto
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