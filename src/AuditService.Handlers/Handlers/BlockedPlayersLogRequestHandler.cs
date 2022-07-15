using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
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

        public BlockedPlayersLogRequestHandler(IMediator mediator)
        {
            _mediator = mediator;
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
            
            return new PageResponseDto<BlockedPlayersLogResponseDto>(response.Pagination, response.List.Select(MapToBlockedPlayersLogResponseDto));
        }

        /// <summary>
        ///     Perform mapping to the DTO model
        /// </summary>
        /// <param name="model">Log of blocked players(Domain model)</param>
        /// <returns>Blocked player log response model</returns>
        private static BlockedPlayersLogResponseDto MapToBlockedPlayersLogResponseDto(BlockedPlayersLogDomainModel model)
            => new()
            {
                BlockingDate = model.BlockingDate,
                PreviousBlockingDate = model.PreviousBlockingDate,
                PlayerLogin = model.PlayerLogin,
                PlayerId = model.PlayerId,
                BlocksCounter = model.BlocksCounter,
                Browser = model.Browser,
                HallId = model.HallId,
                Language = model.Language,
                OperatingSystem = model.Platform,
                PlayerIp = model.LastVisitIpAddress,
                Timestamp = model.Timestamp,
                BrowserVersion = model.BrowserVersion
            };
    }
}