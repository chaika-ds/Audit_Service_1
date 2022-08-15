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
///     Request handler for receiving user visit log
/// </summary>
public class UserVisitLogRequestHandler : IRequestHandler<LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogResponseDto>, PageResponseDto<UserVisitLogResponseDto>>
{
    private readonly IMediator _mediator;
    private readonly SettingsServiceCommands _settingsServiceCommands;

    public UserVisitLogRequestHandler(IMediator mediator, SettingsServiceCommands settingsServiceCommands)
    {
        _mediator = mediator;
        _settingsServiceCommands = settingsServiceCommands;
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

        return new PageResponseDto<UserVisitLogResponseDto>(response.Pagination, await GenerateResponseModelsAsync(response.List, cancellationToken));
    }

    /// <summary>
    ///     Generate response models.
    ///     Formation of a UserVisitLogResponseDto based on a UserVisitLogDomainModel
    /// </summary>
    /// <param name="domainModels">Domain models</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response models</returns>
    private async Task<IEnumerable<UserVisitLogResponseDto>> GenerateResponseModelsAsync(
        IEnumerable<UserVisitLogDomainModel> domainModels, CancellationToken cancellationToken)
    {
        var rootNode = await _settingsServiceCommands.GetCommand<IGetRootNodeTreeCommand>().ExecuteAsync(cancellationToken);

        return from domainModel in domainModels
            join node in rootNode.IncludeChildren() on domainModel.NodeId equals node.Uuid into nodes
            from node in nodes.DefaultIfEmpty()
            select new UserVisitLogResponseDto
            {
                OperatingSystem = domainModel.Authorization.OperatingSystem,
                Browser = domainModel.Authorization.Browser,
                DeviceType = domainModel.Authorization.DeviceType,
                Ip = domainModel.Ip,
                Login = domainModel.Login,
                VisitTime = domainModel.Timestamp,
                NodeId = domainModel.NodeId,
                UserId = domainModel.UserId,
                UserRoles = domainModel.UserRoles,
                NodeName = node?.Title
            };
    }
}