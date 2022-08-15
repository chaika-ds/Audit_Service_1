using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;

namespace AuditService.Handlers.Handlers.ExportRequestHandlers;

/// <summary>
///     Request handler to export the losses log
/// </summary>
public class ExportLossesLogRequestHandler : ExportLogRequestBaseHandler<LossesLogFilterDto, LossesLogSortDto, LossesLogResponseDto>
{
    public ExportLossesLogRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get the name of the exported file
    /// </summary>
    /// <returns>The name of the exported file</returns>
    protected override string GetExportedFileName() => "LossesLog";
}