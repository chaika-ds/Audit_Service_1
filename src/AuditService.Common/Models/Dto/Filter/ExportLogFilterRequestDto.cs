using AuditService.Common.Models.Dto.Sort;
using MediatR;
using Tolar.Export.Enumerations;

namespace AuditService.Common.Models.Dto.Filter;

/// <summary>
///     Request to export logs by filter
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
public class ExportLogFilterRequestDto<TFilter, TSort> : IRequest<ExportFileResponseDto>
    where TFilter : class, ILogFilter, new()
    where TSort : class, ISort, new()
{
    public ExportLogFilterRequestDto()
    {
        Sort = new TSort();
        Filter = new TFilter();
        FileType = ExportType.Csv;
    }

    /// <summary>
    ///     Filter info
    /// </summary>
    public TFilter Filter { get; set; }

    /// <summary>
    ///     Log filter. Sort model
    /// </summary>
    public TSort Sort { get; set; }

    /// <summary>
    ///     Fields to include in the export
    /// </summary>
    public IEnumerable<string>? Columns { get; set; }

    /// <summary>
    ///     File type
    /// </summary>
    public ExportType FileType { get; set; }
}
