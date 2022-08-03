using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Localization.Localizer;
using AuditService.Localization.Localizer.Models;
using MediatR;
using Moq;

namespace AuditService.Tests.AuditService.Handlers.Mock;

/// <summary>
/// Mock interfaces for testing PlayerChangesLogRequestHandler class
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TPlayers">Response type</typeparam>
internal class HandlerMock<TFilter, TSort, TPlayers>
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
    internal IMediator MediatorMock(PageResponseDto<TPlayers> handleResponse, IDictionary<ModuleName, EventDomainModel[]> domainModelResponse)
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
    /// Mock results with exception of Mediator Send method with LogFilterRequestDto params
    /// </summary>
    /// <returns>Mocked Mediator object</returns>
    internal IMediator MediatorLogFilterRequestExceptionMock()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(med =>
                med.Send(It
                        .IsAny<LogFilterRequestDto<TFilter, TSort, TPlayers>>(),
                    It.IsAny<CancellationToken>()).Result).Throws<Exception>();

        return mediatorMock.Object;
    }

    /// <summary>
    /// Mock results with exception of Mediator Send method with GetEventsRequest params
    /// </summary>
    /// <returns>Mocked Mediator object</returns>
    internal IMediator MediatorEventsRequestExceptionMock()
    {
        var mediatorMock = new Mock<IMediator>();
        mediatorMock.Setup(med =>
            med.Send(It
                    .IsAny<GetEventsRequest>(),
                It.IsAny<CancellationToken>()).Result).Throws<Exception>();

        return mediatorMock.Object;
    }

    /// <summary>
    /// Empty Mock results of Mediator Send method
    /// </summary>
    /// <returns>Mocked Mediator object</returns>
    internal IMediator MediatorEmptyMock()
    {
        var mediatorMock = new Mock<IMediator>();
        return mediatorMock.Object;
    }

    /// <summary>
    /// Mock results of Localizer TryLocalize method
    /// </summary>
    /// <param name="localizeResponse">Response for LocalizeKeysRequest data</param>
    /// <returns>Mocked Localizer object</returns>
    internal ILocalizer LocalizerMock(IDictionary<string, string> localizeResponse)
    {
        var localizeMock = new Mock<ILocalizer>();
        localizeMock.Setup(med =>
                med.TryLocalize(It
                    .IsAny<LocalizeKeysRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(localizeResponse));

        return localizeMock.Object;
    }

    /// <summary>
    /// Mock results with exception of Localizer TryLocalize method 
    /// </summary>
    /// <returns>Mocked Localizer object</returns>
    internal ILocalizer LocalizerExceptionMock()
    {
        var localizeMock = new Mock<ILocalizer>();
        localizeMock.Setup(med =>
                med.TryLocalize(It
                    .IsAny<LocalizeKeysRequest>(), It.IsAny<CancellationToken>()))
            .Throws<Exception>();

        return localizeMock.Object;
    }

    /// <summary>
    /// Empty mock results of Localizer TryLocalize method
    /// </summary>
    /// <returns>Mocked Localizer object</returns>
    internal ILocalizer LocalizerEmptyMock()
    {
        var localizeMock = new Mock<ILocalizer>();
        return localizeMock.Object;
    }

}