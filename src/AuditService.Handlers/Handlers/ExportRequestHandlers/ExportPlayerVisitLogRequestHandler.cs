using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Common.Models.Dto.VisitLog;

namespace AuditService.Handlers.Handlers.ExportRequestHandlers;

/// <summary>
///     Request handler to export the player visit log
/// </summary>
public class ExportPlayerVisitLogRequestHandler : ExportLogRequestBaseHandler<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto>
{
    public ExportPlayerVisitLogRequestHandler(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    /// <summary>
    ///     Get the name of the exported file
    /// </summary>
    /// <returns>The name of the exported file</returns>
    protected override string GetExportedFileName() => "PlayersVisitLog";
}
    