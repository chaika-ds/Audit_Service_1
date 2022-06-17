using AuditService.Common.Models.Dto.Pagination;

namespace AuditService.Common.Models.Dto;

/// <summary>
///     Response format
/// </summary>
public class PageResponseDto<T>
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="PageResponseDto{T}" /> class.
    /// </summary>
    public PageResponseDto(int pageSize, int pageNumber, long totalCount, IEnumerable<T> result)
    {
        Pagination = new PaginationResponseDto(totalCount, pageNumber, pageSize);
        List = result;
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="PageResponseDto{T}" /> class.
    /// </summary>
    public PageResponseDto(PaginationRequestDto paginationRequest, long totalCount, IEnumerable<T> result)
    {
        Pagination = new PaginationResponseDto(totalCount, paginationRequest.PageNumber, paginationRequest.PageSize);
        List = result;
    }

    /// <summary>
    ///     Navigation Information
    /// </summary>
    public PaginationResponseDto Pagination { get; }

    /// <summary>
    ///     Array of received objects
    /// </summary>
    public IEnumerable<T> List { get; }
}