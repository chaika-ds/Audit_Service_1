using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using MediatR;

namespace AuditService.Common.Models.Dto
{
    /// <summary>
    /// Request for available services
    /// </summary>
    public record GetServicesRequest : IRequest<IEnumerable<EnumResponseDto>>;

    /// <summary>
    /// Request for available categories by serviceId
    /// </summary>
    /// <param name="ServiceId">Identificator of service</param>
    public record GetCategoriesRequest
        (ServiceStructure? ServiceId = null) : IRequest<IDictionary<ServiceStructure, CategoryDomainModel[]>>;
}
