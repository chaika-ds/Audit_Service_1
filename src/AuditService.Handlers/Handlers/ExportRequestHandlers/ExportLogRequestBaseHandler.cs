using AuditService.Common.Extensions;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Export.Extenisons;
using Tolar.Export.Services;

namespace AuditService.Handlers.Handlers.ExportRequestHandlers;

/// <summary>
///     Base log export request handler
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public abstract class ExportLogRequestBaseHandler<TFilter, TSort, TResponse> : IRequestHandler<ExportLogFilterRequestDto<TFilter, TSort>, ExportDataResponseDto>
    where TFilter : class, ILogFilter, new()
    where TResponse : class
    where TSort : class, ISort, new()
{
    private readonly IMediator _mediator;
    private readonly IExportFactory _exportFactory;

    protected ExportLogRequestBaseHandler(IServiceProvider serviceProvider)
    {
        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _exportFactory = serviceProvider.GetRequiredService<IExportFactory>();
    }

    /// <summary>
    ///     Get the name of the exported file
    /// </summary>
    /// <returns>The name of the exported file</returns>
    protected abstract string GetExportedFileName();

    /// <summary>
    ///     Process log data export request
    /// </summary>
    /// <param name="request">Request model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Export data model</returns>
    public async Task<ExportDataResponseDto> Handle(ExportLogFilterRequestDto<TFilter, TSort> request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new LogFilterRequestDto<TFilter, TSort, TResponse>
            {
                Pagination = request.Pagination,
                Filter = request.Filter,
                Sort = request.Sort
            }, cancellationToken);

        var stream = _exportFactory.GetExporter(request.FileType).Export(response.List, request.Columns?.Select(column => column.ToPascalCase()));
        return new ExportDataResponseDto(request.FileType.GetReportExportFileName(GetExportedFileName()), ((MemoryStream)stream).ToArray(), request.FileType.GetExportContentType());
    }
}