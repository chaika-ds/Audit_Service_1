using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Request handler for receiving player changelog
/// </summary>
public class PlayerChangesLogRequestHandler : IRequestHandler<LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto>,
        PageResponseDto<PlayerChangesLogResponseDto>>
{
    private readonly IMediator _mediator;

    public PlayerChangesLogRequestHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Handle a request to get the player card changelog
    /// </summary>
    /// <param name="request">Request to get the player card changelog</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response model for player card logchanges</returns>
    public async Task<PageResponseDto<PlayerChangesLogResponseDto>> Handle(
        LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto> request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>
            {
                Filter = request.Filter,
                Sort = request.Sort,
                Pagination = request.Pagination
            }, cancellationToken);

        return new PageResponseDto<PlayerChangesLogResponseDto>(response.Pagination,
            response.List.Select(SelectToPlayerChangesLogResponseDto));
    }

    /// <summary>
    ///     Select data to response model(player card logchanges)
    /// </summary>
    /// <param name="model">Changelog in player card</param>
    /// <returns>Response model for player card logchanges</returns>
    private static PlayerChangesLogResponseDto SelectToPlayerChangesLogResponseDto(PlayerChangesLogDomainModel model) =>
        new()
        {
            UserId = model.User.Id,
            UserLogin = model.User.Email,
            EventKey = model.EventCode,
            EventName = "потом тут заполним, когда будет справочник",
            IpAddress = model.IpAddress,
            Reason = model.Reason,
            Timestamp = model.Timestamp,
            NewValue = model.NewValues.Select(SelectToLocalizedPlayerAttribute),
            OldValue = model.OldValues.Select(SelectToLocalizedPlayerAttribute)
        };

    /// <summary>
    ///     Select data to localized player attribute
    /// </summary>
    /// <param name="attribute">User attribute</param>
    /// <returns>Localized attribute player, reflects changed fields</returns>
    private static LocalizedPlayerAttributeDomainModel SelectToLocalizedPlayerAttribute(
        KeyValuePair<string, PlayerAttributeDomainModel> attribute) =>
        new()
        {
            Label = attribute.Key,
            Type = attribute.Value.Type,
            Value = attribute.Value.Value
        };
}