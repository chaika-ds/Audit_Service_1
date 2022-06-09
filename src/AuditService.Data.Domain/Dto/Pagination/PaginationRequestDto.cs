using System.ComponentModel.DataAnnotations;

namespace AuditService.Data.Domain.Dto.Pagination;

/// <summary>
///     Navigation information
/// </summary>
public class PaginationRequestDto
{
    public PaginationRequestDto()
    {
        PageSize = 20;
        PageNumber = 1;
    }

    /// <summary>
    ///     The number of elements per page
    /// </summary>
    [Required]
    public int PageSize { get; set; }

    /// <summary>
    ///     Current page number
    /// </summary>
    [Required]
    public int PageNumber { get; set; }
}