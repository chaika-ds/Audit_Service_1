using System.Diagnostics;
using AuditService.Common.Consts;
using AuditService.Common.Models.Dto;
using KIT.Minio.Settings.Interfaces;
using KIT.NLog.Extensions;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Logging;
using Minio;
using Tolar.MinioService.Client;

namespace KIT.Minio.HealthCheck;

/// <summary>
///     Service to check the health of Minio
/// </summary>
internal class MinioHealthCheck : IMinioHealthCheck
{
    private readonly IFileStorageSettings _settings;
    private readonly IMinioBucketSettings _minioBucketSettings;
    private readonly ILogger<MinioHealthCheck> _logger;

    public MinioHealthCheck(IFileStorageSettings settings, IMinioBucketSettings minioBucketSettings, ILogger<MinioHealthCheck> logger)
    {
        _settings = settings;
        _minioBucketSettings = minioBucketSettings;
        _logger = logger;
    }

    /// <summary>
    ///     Check the health of the Minio service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Represents the result of a health check with millisecond</returns>
    public async Task<HealthCheckComponentsDto> CheckHealthAsync(CancellationToken cancellationToken = default)
    {
        var stopwatch = Stopwatch.StartNew();
        var healthCheckResult = await CheckHealthResultAsync(cancellationToken);
        stopwatch.Stop();

        return new HealthCheckComponentsDto
        {
            Name = HealthCheckConst.Minio,
            RequestTime = stopwatch.ElapsedMilliseconds,
            Status = healthCheckResult.Status == HealthStatus.Healthy,
        };
    }

    /// <summary>
    ///     Check the health of the Minio service
    /// </summary>
    /// <returns>Represents the result of a health check</returns>
    private async Task<HealthCheckResult> CheckHealthResultAsync(CancellationToken cancellationToken)
    {
        try
        {
            var minioClient = CreateMinioClient();
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(_minioBucketSettings.BucketName);
            var isExists = await minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
            return isExists ? HealthCheckResult.Healthy() : HealthCheckResult.Degraded();
        }
        catch (Exception ex)
        {
            _logger.LogException(ex, $"Check Minio healthy on {DateTime.UtcNow}");
            return HealthCheckResult.Unhealthy(ex.Message, ex);
        }
    }

    /// <summary>
    ///     Create minio client
    /// </summary>
    /// <returns>Minio client</returns>
    private MinioClient CreateMinioClient()
    {
        var minioClient = new MinioClient()
            .WithEndpoint(_settings.Endpoint)
            .WithCredentials(_settings.AccessKey, _settings.SecretKey)
            .Build();

        if (_settings.WithSSL)
            minioClient.WithSSL();

        return minioClient;
    }
}