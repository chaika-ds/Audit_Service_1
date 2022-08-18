using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Helpers.ExportLogTest;
using AuditService.Tests.Resources;
using Tolar.Export.Enumerations;

namespace AuditService.Tests.Tests.Journals.ExportLog;

public class ExportVisitLogTest
{
    /// <summary>
    ///     Check if the result is coming from export players visit log handler
    /// </summary>
    [Fact]
    public async Task GetLinkOnDocument_CreatePlayerVisitLog_ResultReturned()
    {
        await ExportLogTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
            .CheckReturnResult(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);
    }

    /// <summary>
    ///     Check if the result is coming from export users visit log handler
    /// </summary>
    [Fact]
    public async Task GetLinkOnDocument_CreateUserVisitLog_ResultReturned()
    {
        await ExportLogTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
            .CheckReturnResult(TestResources.VisitLog, TestResources.ElasticSearchVisitLogResponse);
    }

    /// <summary>
    ///     Check if the document created
    /// </summary>
    [Fact]
    public async Task GetLinkOnDocument_CreateUserVisitLog_DocumentCreated()
    {
        await ExportLogTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
            .CheckCreateDocument(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse);
    }

    /// <summary>
    ///     Check if the document extantion is csv
    /// </summary>
    [Fact]
    public async Task GetDocumentExtension_CreateUserVisitLog_DocumentExtensionIsCsv()
    {
        await ExportLogTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
            .CheckDocumentExtension(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse, TestResources.CsvExtension, ExportType.Csv);
    }

    /// <summary>
    ///     Check if the document extantion is elsx
    /// </summary>
    [Fact]
    public async Task GetDocumentExtension_CreateUserVisitLog_DocumentExtensionIsElsx()
    {
        await ExportLogTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
            .CheckDocumentExtension(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse, TestResources.ExcelExtension, ExportType.Excel);
    }

    /// <summary>
    ///     Check if the exception is coming from players visit log domain handler
    /// </summary>
    [Fact]
    public async Task GetLinkOnDocument_CreateVisitLog_ThrowUnsupportedExportTypeExecption()
    {
        await ExportLogTestHelper<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>
            .CheckCreateDocumentThrow(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse);
    }
}
