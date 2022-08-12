using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Common.Models.Dto.VisitLog;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Request handler for receiving player visit log
/// </summary>
public class PlayerVisitLogRequestHandler : IRequestHandler<LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto>, 
    PageResponseDto<PlayerVisitLogResponseDto>>
{
    private readonly IMediator _mediator;

    public PlayerVisitLogRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Handle a request to get the players visit log
    /// </summary>
    /// <param name="request">Request to players visit log</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response model for players visit log</returns>
    public async Task<PageResponseDto<PlayerVisitLogResponseDto>> Handle(
        LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto> request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
            {
                Pagination = request.Pagination,
                Filter = request.Filter,
                Sort = request.Sort
            }, cancellationToken);

        return new PageResponseDto<PlayerVisitLogResponseDto>(response.Pagination, response.List.Select(MapToPlayerVisitLogResponseDto));
    }

    /// <summary>
    ///     Map to response DTO model
    /// </summary>
    /// <param name="model">Player visit log</param>
    /// <returns>Player visit log(DTO)</returns>
    private PlayerVisitLogResponseDto MapToPlayerVisitLogResponseDto(PlayerVisitLogDomainModel model)
        => new()
        {
            PlayerId = model.PlayerId,
            NodeId = model.NodeId,
            OperatingSystem = model.Authorization.OperatingSystem,
            Browser = model.Authorization.Browser,
            AuthorizationMethod = model.Authorization.AuthorizationType!,
            DeviceType = model.Authorization.DeviceType,
            Ip = model.Ip,
            Login = model.Login,
            VisitTime = model.Timestamp
        };
}