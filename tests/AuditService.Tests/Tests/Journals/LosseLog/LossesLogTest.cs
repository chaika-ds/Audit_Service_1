using AuditService.Common.Models.Domain.LossesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Helpers.Journals;
using AuditService.Tests.Resources;

namespace AuditService.Tests.Tests.Journals.LosseLog
{
    public class LossesLogTest
    {
        /// <summary>
        ///  Expected losses log
        /// </summary>
        private readonly List<LossesLogDomainModel> _expectedLossesLog;

        public LossesLogTest()
        {
            _expectedLossesLog = LogsTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel, LossesLogDomainModel>
                .GetExpectedDomainModels(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse);
        }

        /// <summary>
        ///     Check if the result is coming from losses log domain handler
        /// </summary>
        [Fact]
        public async Task GetLossesLogs_CreateVisitLog_ResultWithLossesDomainLogs()
        {
            await LogsTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel, LossesLogDomainModel>
                .CheckReturnResult(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse);
        }

        /// <summary>
        ///     Validation of losses log domain response
        /// </summary>
        [Fact]
        public async Task LossesLogDomainResponseValidation_CreateVisitLog_HandlerDomainResponseCorrespondsToTheExpected()
        {
            //Arrange
            var expected = _expectedLossesLog
               ?.FirstOrDefault();

            //Act 
            var result = await LogsTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogDomainModel, LossesLogDomainModel>
            .GetLogHandlerResponse(TestResources.PlayerChangesLog, TestResources.ElasticSearchLossesLogResponse);

            var actual = result.List.FirstOrDefault(x => x.PlayerId == expected.PlayerId);

            //Assert
            Equal(expected.Login, actual.Login);
            Equal(expected.CreateDate, actual.CreateDate);
            Equal(expected.PlayerId, actual.PlayerId);
            Equal(expected.NodeId, actual.NodeId);
            Equal(expected.CurrencyCode, actual.CurrencyCode);
            Equal(expected.LastDeposit, actual.LastDeposit);
        }

        /// <summary>
        ///     Check if the result is coming from losses log dto handler
        /// </summary>
        [Fact]
        public async Task GetLossesLogs_CreateVisitLog_ResultWithLossesDtoLogs()
        {
            await LogsTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogResponseDto, LossesLogDomainModel>
                .CheckReturnResult(TestResources.LossesLog, TestResources.ElasticSearchLossesLogResponse);
        }

        /// <summary>
        ///     Validation of losses log dto response
        /// </summary>
        [Fact]
        public async Task PlayerLossesLogResponseMappingCheck_CreateVisitLog_HandlerDtoResponseCorrespondsToTheExpected()
        {
            //Arrange
            var expected = _expectedLossesLog
               ?.FirstOrDefault();

            //Act 
            var result = await LogsTestHelper<LossesLogFilterDto, LossesLogSortDto, LossesLogResponseDto, LossesLogDomainModel>
            .GetLogHandlerResponse(TestResources.PlayerChangesLog, TestResources.ElasticSearchLossesLogResponse);

            var actual = result.List.FirstOrDefault(x => x.PlayerId == expected.PlayerId);

            //Assert
            Equal(expected.Login, actual.Login);
            Equal(expected.CreateDate, actual.CreatedTime);
            Equal(expected.PlayerId, actual.PlayerId);
            Equal(expected.NodeId, actual.NodeId);
            Equal(expected.CurrencyCode, actual.CurrencyCode);
            Equal(expected.LastDeposit, actual.LastDeposit);
        }
    }
}
