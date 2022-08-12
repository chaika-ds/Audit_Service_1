using MediatR;

namespace AuditService.Common.Models.Dto
{
    /// <summary>
    /// Redis service health check request
    /// </summary>
    public record CheckHealthRequest : IRequest<HealthCheckResponseDto>;
}
