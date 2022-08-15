using AuditService.Common.Models.Domain.LossesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.PipelineBehaviors.Attributes;
using AuditService.SettingsService.Commands.BaseEntities;
using AuditService.SettingsService.Commands.GetRootNodeTree;
using AuditService.SettingsService.Extensions;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Request handler for receiving losses log
/// </summary>
[UsePipelineBehaviors(UseLogging = true, UseCache = true, CacheLifeTime = 120, UseValidation = true)]
public class LossesLogRequestHandler : IRequestHandler<LogFilterRequestDto<LossesLogFilterDto, LossesLogSortDto, LossesLogResponseDto>, PageResponseDto<LossesLogResponseDto>>
{
    private readonly IMediator _mediator;
    private readonly SettingsServiceCommands _settingsServiceCommands;

    public LossesLogRequestHandler(IMediator mediator, SettingsServiceCommands settingsServiceCommands)
    {
        _mediator = mediator;
        _settingsServiceCommands = settingsServiceCommands;
    }

    /// <summary>
    ///     Handle a request to get the losses log
    /// </summary>
    /// <param name="request">Request to get the losses log</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response model for losses log</returns>
    public async Task<PageResponseDto<LossesLogResponseDto>> Handle(LogFilterRequestDto<LossesLogFilterDto, LossesLogSortDto, LossesLogResponseDto> request, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LogFilterRequestDto<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel>
        {
            Sort = request.Sort,
            Pagination = request.Pagination,
            Filter = request.Filter
        }, cancellationToken);

        return new PageResponseDto<LossesLogResponseDto>(response.Pagination, await GenerateResponseModelsAsync(response.List, cancellationToken));
    }

    /// <summary>
    ///     Generate response models.
    ///     Formation of a LossesLogResponseDto based on a LossesLogDomainModel
    /// </summary>
    /// <param name="domainModels">Domain models</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response models</returns>
    private async Task<IEnumerable<LossesLogResponseDto>> GenerateResponseModelsAsync(
        IEnumerable<LossesLogDomainModel> domainModels, CancellationToken cancellationToken)
    {
        var rootNode = await _settingsServiceCommands.GetCommand<IGetRootNodeTreeCommand>().ExecuteAsync(cancellationToken);

        return from domainModel in domainModels
            join node in rootNode.IncludeChildren() on domainModel.NodeId equals node.Uuid into nodes
            from node in nodes.DefaultIfEmpty()
            select new LossesLogResponseDto
            {
                PlayerId = domainModel.PlayerId,
                NodeId = domainModel.NodeId,
                Login = domainModel.Login,
                CreatedTime = domainModel.CreateDate,
                CurrencyCode = domainModel.CurrencyCode,
                LastDeposit = domainModel.LastDeposit,
                NodeName = node?.Title
            };
    }
}