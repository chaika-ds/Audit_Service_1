using MediatR;

namespace AuditService.Common.Models.Dto
{
    /// <summary>
    /// Elk service health check request
    /// </summary>
    public record CheckElkHealthRequest : IRequest<bool>;

    /// <summary>
    /// Kafka service health check request
    /// </summary>
    public record CheckKafkaHealthRequest : IRequest<bool>;
}
