using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.SettingsService.Commands.BaseEntities;
using AuditService.SettingsService.Commands.GetRootNodeTree;
using AuditService.SettingsService.Extensions;
using MediatR;

namespace AuditService.Handlers.Handlers
{
    /// <summary>
    ///     Request handler for receiving blocked players log
    /// </summary>
    public class BlockedPlayersLogRequestHandler :
        IRequestHandler<LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto>, PageResponseDto<BlockedPlayersLogResponseDto>>
    {
        private readonly IMediator _mediator;
        private readonly SettingsServiceCommands _settingsServiceCommands;

        public BlockedPlayersLogRequestHandler(IMediator mediator, SettingsServiceCommands settingsServiceCommands)
        {
            _mediator = mediator;
            _settingsServiceCommands = settingsServiceCommands;
        }

        /// <summary>
        ///     Handle a request to get the blocked players log
        /// </summary>
        /// <param name="request">Request to get the blocked players log</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response model for blocked players log</returns>
        public async Task<PageResponseDto<BlockedPlayersLogResponseDto>> Handle(
            LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto> request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogDomainModel>
                {
                    Sort = request.Sort,
                    Pagination = request.Pagination,
                    Filter = request.Filter
                }, cancellationToken);
            
            return new PageResponseDto<BlockedPlayersLogResponseDto>(response.Pagination, await GenerateResponseModelsAsync(response.List, cancellationToken));
        }

        /// <summary>
        ///     Generate response models.
        ///     Formation of a BlockedPlayersLogResponseDto based on a BlockedPlayersLogDomainModel
        /// </summary>
        /// <param name="domainModels">Domain models</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Response models</returns>
        private async Task<IEnumerable<BlockedPlayersLogResponseDto>> GenerateResponseModelsAsync(
            IEnumerable<BlockedPlayersLogDomainModel> domainModels, CancellationToken cancellationToken)
        {
            var rootNode = await _settingsServiceCommands.GetCommand<IGetRootNodeTreeCommand>().ExecuteAsync(cancellationToken);

            return from domainModel in domainModels
                join node in rootNode.IncludeChildren() on domainModel.NodeId equals node.Uuid into nodes
                from node in nodes.DefaultIfEmpty()
                select new BlockedPlayersLogResponseDto
                {
                    BlockingDate = domainModel.BlockingDate,
                    PreviousBlockingDate = domainModel.PreviousBlockingDate,
                    PlayerLogin = domainModel.PlayerLogin,
                    PlayerId = domainModel.PlayerId,
                    BlocksCounter = domainModel.BlocksCounter,
                    Browser = domainModel.Browser,
                    NodeId = domainModel.NodeId,
                    Language = domainModel.Language,
                    OperatingSystem = domainModel.Platform,
                    PlayerIp = domainModel.LastVisitIpAddress,
                    Timestamp = domainModel.Timestamp,
                    BrowserVersion = domainModel.BrowserVersion,
                    NodeName = node?.Title
                };
        }
    }
}