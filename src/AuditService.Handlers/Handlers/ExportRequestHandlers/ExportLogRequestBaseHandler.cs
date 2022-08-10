using AuditService.Common.Extensions;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using KIT.Minio.Commands.SaveFileWithSharing;
using KIT.Minio.Commands.SaveFileWithSharing.Models;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Tolar.Export.Enumerations;
using Tolar.Export.Extenisons;
using Tolar.Export.Services;

namespace AuditService.Handlers.Handlers.ExportRequestHandlers;

/// <summary>
///     Base log export request handler
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TResponse">Response type</typeparam>
public abstract class ExportLogRequestBaseHandler<TFilter, TSort, TResponse> : IRequestHandler<ExportLogFilterRequestDto<TFilter, TSort>, ExportFileResponseDto>
    where TFilter : class, ILogFilter, new()
    where TResponse : class
    where TSort : class, ISort, new()
{
    private readonly IMediator _mediator;
    private readonly IExportFactory _exportFactory;
    private readonly ISaveFileWithSharingCommand _saveFileWithSharingCommand;

    protected ExportLogRequestBaseHandler(IServiceProvider serviceProvider)
    {
        _mediator = serviceProvider.GetRequiredService<IMediator>();
        _exportFactory = serviceProvider.GetRequiredService<IExportFactory>();
        _saveFileWithSharingCommand = serviceProvider.GetRequiredService<ISaveFileWithSharingCommand>();
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
    public async Task<ExportFileResponseDto> Handle(ExportLogFilterRequestDto<TFilter, TSort> request,
        CancellationToken cancellationToken)
    {
        var logEntries = await GetLogEntriesAsync(request, new PaginationRequestDto
        {
            PageNumber = 1,
            PageSize = 10000
        }, cancellationToken);

        var stream = _exportFactory.GetExporter(request.FileType).Export(logEntries, request.Columns?.Select(column => column.ToPascalCase()));
        var linkToFile = await _saveFileWithSharingCommand.ExecuteAsync(GenerateSaveFileWithSharingModel(stream, request.FileType), cancellationToken);
        return new ExportFileResponseDto(linkToFile);
    }

    /// <summary>
    ///     Get log entries
    /// </summary>
    /// <param name="request">Request model</param>
    /// <param name="pagination">Pagination model</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Log entries</returns>
    private async Task<IEnumerable<TResponse>> GetLogEntriesAsync(ExportLogFilterRequestDto<TFilter, TSort> request, PaginationRequestDto pagination, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new LogFilterRequestDto<TFilter, TSort, TResponse>
        {
            Pagination = pagination,
            Filter = request.Filter,
            Sort = request.Sort
        }, cancellationToken);

        if (response.Pagination.PageCount == response.Pagination.PageNumber)
            return response.List;

        pagination.PageNumber = response.Pagination.PageNumber + 1;
        var nextEntriesPortion = await GetLogEntriesAsync(request, pagination, cancellationToken);
        return response.List.Union(nextEntriesPortion);
    }

    /// <summary>
    ///     Generate model to save file with sharing
    /// </summary>
    /// <param name="stream">File stream</param>
    /// <param name="exportType">Export type</param>
    /// <returns>Model to save file with sharing</returns>
    private SaveFileWithSharingModel GenerateSaveFileWithSharingModel(Stream stream, ExportType exportType) =>
        new(stream, exportType.GetReportExportFileName(GetExportedFileName()), exportType.GetExportContentType());
}