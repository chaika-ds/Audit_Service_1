using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Mappers;
using MediatR;

namespace AuditService.Handlers.Handlers
{
    /// <summary>
    ///     Request handler for receiving blocked players log
    /// </summary>
    public class BlockedPlayersLogRequestHandler :
        IRequestHandler<LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto, BlockedPlayersLogResponseDto>,
            PageResponseDto<BlockedPlayersLogResponseDto>>
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
            var response = await _mediator.Send(
                new LogFilterRequestDto<BlockedPlayersLogFilterDto, BlockedPlayersLogSortDto,
                    BlockedPlayersLogDomainModel>
                        {
                            Sort = request.Sort,
                            Pagination = request.Pagination,
                            Filter = request.Filter
                        }, cancellationToken);


            return new PageResponseDto<BlockedPlayersLogResponseDto>(response.Pagination,
                response.List.Select(element => element.MapToBlockedPlayersLogResponseDto()));
        }
    }
}