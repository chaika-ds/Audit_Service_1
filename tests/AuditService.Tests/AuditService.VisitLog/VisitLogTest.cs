using AuditService.Common.Models.Domain.VisitLog;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Filter.VisitLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Common.Models.Dto.VisitLog;
using AuditService.Tests.Fakes;
using AuditService.Tests.Resources;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.Tests.AuditService.VisitLog
{
    /// <summary>
    ///     Test of PlayerVisitLogDomainRequestHandler
    /// </summary> 
    public class VisitLogTest
    {
        /// <summary>
        ///     Check if the result is coming from players visit log domain handler
        /// </summary>
        [Fact]
        public async Task GetPlayerVisiLogs_CreateVisitLog_ResultWithPlayerDomainLogs()
        {
            //Arrange
            var serviceProvider = FakeServiceProvider
                .GetServiceProviderForLogHandlers<PlayerVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

            var auditLogDomainRequestHandler = serviceProvider.GetRequiredService<IMediator>();

            var filter = new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogDomainModel>();

            //Act 
            var result = await auditLogDomainRequestHandler.Send(filter, new TaskCanceledException().CancellationToken);

            //Assert
            Assert.True(result.List.Any());
        }

        /// <summary>
        ///     Check if the result is coming from users visit log domain handler
        /// </summary>
        [Fact]
        public async Task GetUserVisiLogs_CreateVisitLog_ResultWithUserDomainLogs()
        {
            //Arrange
            var serviceProvider = FakeServiceProvider
                .GetServiceProviderForLogHandlers<UserVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

            var auditLogDomainRequestHandler = serviceProvider.GetRequiredService<IMediator>();

            var filter = new LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogDomainModel>();

            //Act 
            var result = await auditLogDomainRequestHandler.Send(filter, new TaskCanceledException().CancellationToken);

            //Assert
            Assert.True(result.List.Any());
        }

        /// <summary>
        ///     Check if the result is coming from players visit log handler
        /// </summary>
        [Fact]
        public async Task GetPlayerVisiLogs_CreateVisitLog_ResultWithPlayerDroLogs()
        {
            //Arrange
            var serviceProvider = FakeServiceProvider
                .GetServiceProviderForLogHandlers<PlayerVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

            var auditLogDomainRequestHandler = serviceProvider.GetRequiredService<IMediator>();

            var filter = new LogFilterRequestDto<PlayerVisitLogFilterDto, PlayerVisitLogSortDto, PlayerVisitLogResponseDto>();

            //Act 
            var result = await auditLogDomainRequestHandler.Send(filter, new TaskCanceledException().CancellationToken);

            //Assert
            Assert.True(result.List.Any());
        }

        /// <summary>
        ///     Check_if the result is coming from users visit log handler
        /// </summary>
        [Fact]
        public async Task GetUserVisiLogs_CreateVisitLog_ResultWithUserDtoLogs()
        {
            //Arrange
            var serviceProvider = FakeServiceProvider
                .GetServiceProviderForLogHandlers<UserVisitLogDomainModel>(TestResources.ElasticSearchVisitLogResponse, TestResources.VisitLog);

            var auditLogDomainRequestHandler = serviceProvider.GetRequiredService<IMediator>();

            var filter = new LogFilterRequestDto<UserVisitLogFilterDto, UserVisitLogSortDto, UserVisitLogResponseDto>();

            //Act 
            var result = await auditLogDomainRequestHandler.Send(filter, new TaskCanceledException().CancellationToken);

            //Assert
            Assert.True(result.List.Any());
        }
    }
}
