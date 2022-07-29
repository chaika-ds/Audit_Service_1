using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Common.Models.Dto.VisitLog;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Request handler for receiving user visit log
/// </summary>
public class UserVisitLogRequestHandler : IRequestHandler<LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogResponseDto>, PageResponseDto<UserVisitLogResponseDto>>
{
    private readonly IMediator _mediator;

    public UserVisitLogRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Handle a request to get the users visit log
    /// </summary>
    /// <param name="request">Request to users visit log</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response model for users visit log</returns>
    public async Task<PageResponseDto<UserVisitLogResponseDto>> Handle(
        LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogResponseDto> request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel>
        {
            Pagination = request.Pagination,
            Filter = request.Filter,
            Sort = request.Sort
        }, cancellationToken);

        return new PageResponseDto<UserVisitLogResponseDto>(response.Pagination, response.List.Select(MapToUserVisitLogResponseDto));
    }

    /// <summary>
    ///     Map to response DTO model
    /// </summary>
    /// <param name="model">User visit log</param>
    /// <returns>User visit log(DTO)</returns>
    private UserVisitLogResponseDto MapToUserVisitLogResponseDto(UserVisitLogDomainModel model)
        => new()
        {
            OperatingSystem = model.Authorization.OperatingSystem,
            Browser = model.Authorization.Browser,
            DeviceType = model.Authorization.DeviceType,
            Ip = model.Ip,
            Login = model.Login,
            VisitTime = model.Timestamp,
            NodeId = model.NodeId,
            UserId = model.UserId,
            UserRoles = model.UserRoles
        };
}