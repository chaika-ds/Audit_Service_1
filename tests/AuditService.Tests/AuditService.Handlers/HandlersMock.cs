using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Localization.Localizer;
using AuditService.Localization.Localizer.Models;
using MediatR;
using Moq;

namespace AuditService.Tests.AuditService.Handlers;

/// <summary>
/// Mock interfaces for testing PlayerChangesLogRequestHandler class
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TPlayers">Response type</typeparam>
public class HandlersMock<TFilter, TSort, TPlayers>
    where TFilter : class, new()
    where TSort : class, ISort, new()
    where TPlayers : class
{
    /// <summary>
    /// Mock results of Mediator Send method
    /// </summary>
    /// <param name="handleResponse">Response for LogFilterRequestDto data</param>
    /// <param name="domainModelResponse">Response for GetEventsRequest data</param>
    /// <returns>Mocked Mediator object</returns>
    protected IMediator MediatorMock(PageResponseDto<TPlayers> handleResponse, IDictionary<ModuleName, EventDomainModel[]> domainModelResponse)
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(med =>
                med.Send(It
                        .IsAny<LogFilterRequestDto<TFilter, TSort, TPlayers>>(),
                    It.IsAny<CancellationToken>()).Result)
            .Returns(handleResponse);

        mediatorMock.Setup(med =>
                med.Send(It
                        .IsAny<GetEventsRequest>(),
                    It.IsAny<CancellationToken>()).Result)
            .Returns(domainModelResponse);

        return mediatorMock.Object;
    }

    /// <summary>
    /// Mock results of Localizer TryLocalize method
    /// </summary>
    /// <param name="localizeResponse">Response for LocalizeKeysRequest data</param>
    /// <returns>Mocked Localizer object</returns>
    protected ILocalizer LocalizerMock(IDictionary<string, string> localizeResponse)
    {
        var localizeMock = new Mock<ILocalizer>();
        localizeMock.Setup(med =>
                med.TryLocalize(It
                    .IsAny<LocalizeKeysRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(localizeResponse));

        return localizeMock.Object;
    }

}