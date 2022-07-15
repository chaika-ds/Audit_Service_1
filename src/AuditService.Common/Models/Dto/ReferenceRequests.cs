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
/// <param name="ModuleName">Identificator of service</param>
[UseCache(Lifetime = 600)]
public record GetCategoriesRequest
    (ModuleName? ModuleName = null) : IRequest<IDictionary<ModuleName, CategoryDomainModel[]>>;

/// <summary>
///     Request for available actions
/// </summary>
[UseCache(Lifetime = 600)]
public record GetActionsRequest (string CategoryCode)  : IRequest<IEnumerable<ActionDomainModel>?>;

/// <summary>
///     Request for available events
/// </summary>
[UseCache(Lifetime = 600)]
public record GetEventsRequest(ModuleName? ModuleName = null): IRequest<IDictionary<ModuleName, EventDomainModel[]>>;
