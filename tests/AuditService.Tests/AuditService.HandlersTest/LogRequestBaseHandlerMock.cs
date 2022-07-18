using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Nest;

namespace AuditService.Tests.AuditService.HandlersTest
{
    public class LogRequestBaseHandlerMock
    {
        [Fact]
        public async Task LogRequestBaseHandler_ServicesInjection_Injected()
        {

            var tcs = new CancellationTokenSource(1000);
            var handler =
                new Mock<IRequestHandler<
                    LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>,
                    PageResponseDto<PlayerChangesLogDomainModel>>>();
            handler.Object.Handle(
                new LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>(),
                tcs.Token);
        }
    }
}
