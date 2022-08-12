using AuditService.Common.Models.Domain.AuditLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.SettingsService.Commands.BaseEntities;
using AuditService.SettingsService.Commands.GetRootNodeTree;
using AuditService.SettingsService.Extensions;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Request handler for receiving audit log
/// </summary>
public class AuditLogRequestHandler : IRequestHandler<LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogResponseDto>, PageResponseDto<AuditLogResponseDto>>
{
    private readonly IMediator _mediator;
    private readonly SettingsServiceCommands _settingsServiceCommands;

    public AuditLogRequestHandler(IMediator mediator, SettingsServiceCommands settingsServiceCommands)
    {
        _mediator = mediator;
        _settingsServiceCommands = settingsServiceCommands;
    }

    /// <summary>
    ///     Handle a request to get the audit log
    /// </summary>
    /// <param name="request">Request to get the audit log</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response model for audit log</returns>
    public async Task<PageResponseDto<AuditLogResponseDto>> Handle(LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogResponseDto> request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LogFilterRequestDto<AuditLogFilterDto, LogSortDto, AuditLogDomainModel>
        {
            Sort = request.Sort,
            Pagination = request.Pagination,
            Filter = request.Filter
        }, cancellationToken);

        return new PageResponseDto<AuditLogResponseDto>(response.Pagination, await GenerateResponseModelsAsync(response.List, cancellationToken));
    }

    /// <summary>
    ///     Generate response models.
    ///     Formation of a AuditLogResponseDto based on a AuditLogDomainModel
    /// </summary>
    /// <param name="domainModels">Domain models</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response models</returns>
    private async Task<IEnumerable<AuditLogResponseDto>> GenerateResponseModelsAsync(IEnumerable<AuditLogDomainModel> domainModels, CancellationToken cancellationToken)
    {
        var rootNode = await _settingsServiceCommands.GetCommand<IGetRootNodeTreeCommand>().ExecuteAsync(cancellationToken);
        return from domainModel in domainModels
            join node in rootNode.IncludeChildren() on domainModel.NodeId equals node.Uuid into nodes
            from node in nodes.DefaultIfEmpty()
            select new AuditLogResponseDto
            {
                Timestamp = domainModel.Timestamp,
                NodeId = domainModel.NodeId,
                User = domainModel.User,
                ModuleName = domainModel.ModuleName,
                ActionName = domainModel.ActionName,
                CategoryCode = domainModel.CategoryCode,
                EntityId = domainModel.EntityId,
                EntityName = domainModel.EntityName,
                NewValue = domainModel.NewValue,
                OldValue = domainModel.OldValue,
                RequestBody = domainModel.RequestBody,
                RequestUrl = domainModel.RequestUrl,
                NodeType = node?.Type
            };
    }
}