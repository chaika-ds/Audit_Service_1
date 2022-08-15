using AuditService.Common.Models.Dto;

namespace KIT.Minio.HealthCheck;

/// <summary>
///     Service to check the health of Minio
/// </summary>
public interface IMinioHealthCheck
{
    /// <summary>
    ///     Check the health of the Minio service
    /// </summary>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Represents the result of a health check with millisecond</returns>
    Task<HealthCheckComponentsDto> CheckHealthAsync(CancellationToken cancellationToken = default);
}