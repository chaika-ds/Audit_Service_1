using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Common.Models.Dto.VisitLog;
using AuditService.SettingsService.Commands.BaseEntities;
using AuditService.SettingsService.Commands.GetRootNodeTree;
using AuditService.SettingsService.Extensions;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Request handler for receiving player visit log
/// </summary>
public class PlayerVisitLogRequestHandler : IRequestHandler<LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto>, 
    PageResponseDto<PlayerVisitLogResponseDto>>
{
    private readonly IMediator _mediator;
    private readonly SettingsServiceCommands _settingsServiceCommands;

    public PlayerVisitLogRequestHandler(IMediator mediator, SettingsServiceCommands settingsServiceCommands)
    {
        _mediator = mediator;
        _settingsServiceCommands = settingsServiceCommands;
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

        return new PageResponseDto<PlayerVisitLogResponseDto>(response.Pagination, await GenerateResponseModelsAsync(response.List, cancellationToken));
    }

    /// <summary>
    ///     Generate response models.
    ///     Formation of a PlayerVisitLogResponseDto based on a PlayerVisitLogDomainModel
    /// </summary>
    /// <param name="domainModels">Domain models</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response models</returns>
    private async Task<IEnumerable<PlayerVisitLogResponseDto>> GenerateResponseModelsAsync(
        IEnumerable<PlayerVisitLogDomainModel> domainModels, CancellationToken cancellationToken)
    {
        var rootNode = await _settingsServiceCommands.GetCommand<IGetRootNodeTreeCommand>().ExecuteAsync(cancellationToken);

        return from domainModel in domainModels
            join node in rootNode.IncludeChildren() on domainModel.NodeId equals node.Uuid into nodes
            from node in nodes.DefaultIfEmpty()
            select new PlayerVisitLogResponseDto
            {
                PlayerId = domainModel.PlayerId,
                NodeId = domainModel.NodeId,
                OperatingSystem = domainModel.Authorization.OperatingSystem,
                Browser = domainModel.Authorization.Browser,
                AuthorizationMethod = domainModel.Authorization.AuthorizationType!,
                DeviceType = domainModel.Authorization.DeviceType,
                Ip = domainModel.Ip,
                Login = domainModel.Login,
                VisitTime = domainModel.Timestamp,
                NodeName = node?.Title
            };
    }
}