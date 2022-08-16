using AuditService.Common.Models.Domain.LossesLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Helpers.ExportLogTest;
using AuditService.Tests.Resources;
using Tolar.Export.Enumerations;

namespace AuditService.Tests.Tests.Journals.ExportLog
{
    public class ExportLossesLogTest
    {
        /// <summary>
        ///     Check if the result is coming from export losses log handler
        /// </summary>
        [Fact]
        public async Task GetLinkOnDocument_CreatePlayerVisitLog_ResultReturned()
        {
            await ExportLogTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel>
                .CheckReturnResult(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse);
        }

        /// <summary>
        ///     Check if the document created
        /// </summary>
        [Fact]
        public async Task GetLinkOnDocument_CreatelossesLog_DocumentCreated()
        {
            await ExportLogTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel>
                .CheckCreateDocument(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse);
        }

        /// <summary>
        ///     Check if the document extantion is csv
        /// </summary>
        [Fact]
        public async Task GetDocumentExtension_CreatelossesLog_DocumentExtensionIsCsv()
        {
            await ExportLogTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel>
                .CheckDocumentExtension(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse, TestResources.CsvExtension, ExportType.Csv);
        }

        /// <summary>
        ///     Check if the document extantion is elsx
        /// </summary>
        [Fact]
        public async Task GetDocumentExtension_CreatelossesLog_DocumentExtensionIsElsx()
        {
            await ExportLogTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel>
                .CheckDocumentExtension(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse, TestResources.ExcelExtension, ExportType.Excel);
        }

        /// <summary>
        ///     Check if the exception is coming from export losses log domain handler
        /// </summary>
        [Fact]
        public async Task GetLinkOnDocument_CreateVisitLog_ThrowUnsupportedExportTypeExecption()
        {
            await ExportLogTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel>
                .CheckCreateDocumentThrow(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse);
        }
    }
}