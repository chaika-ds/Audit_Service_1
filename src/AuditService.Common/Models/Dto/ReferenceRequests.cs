using AuditService.Common.Attributes;
using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using MediatR;

namespace AuditService.Common.Models.Dto;

/// <summary>
///     Request for available services
/// </summary>
[UseCache(Lifetime = 600)]
public record GetServicesRequest : IRequest<IEnumerable<EnumResponseDto>>;

/// <summary>
///     Request for available categories by serviceId
/// </summary>
/// <param name="ServiceId">Identificator of service</param>
[UseCache(Lifetime = 600)]
public record GetCategoriesRequest
    (ServiceStructure? ServiceId = null) : IRequest<IDictionary<ServiceStructure, CategoryDomainModel[]>>;

/// <summary>
///     Request for available actions
/// </summary>
[UseCache(Lifetime = 600)]
public record GetActionsRequest : IRequest<IEnumerable<EnumResponseDto>>;