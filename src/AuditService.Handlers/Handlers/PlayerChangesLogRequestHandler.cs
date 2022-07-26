using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.PipelineBehaviors.Attributes;
using AuditService.Localization.Localizer;
using AuditService.Localization.Localizer.Models;
using MediatR;

namespace AuditService.Handlers.Handlers;

/// <summary>
///     Request handler for receiving player changelog
/// </summary>
[UsePipelineBehaviors(UseLogging = true, UseCache = true, CacheLifeTime = 120)]
public class PlayerChangesLogRequestHandler : IRequestHandler<
    LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto>,
    PageResponseDto<PlayerChangesLogResponseDto>>
{
    private readonly IMediator _mediator;
    private readonly ILocalizer _localizer;

    public PlayerChangesLogRequestHandler(IMediator mediator, ILocalizer localizer)
    {
        _mediator = mediator;
        _localizer = localizer;
    }

    /// <summary>
    ///     Handle a request to get the player card changelog
    /// </summary>
    /// <param name="request">Request to get the player card changelog</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response model for player card logchanges</returns>
    public async Task<PageResponseDto<PlayerChangesLogResponseDto>> Handle(
        LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto> request,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(
            new LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogDomainModel>
            {
                Filter = request.Filter,
                Sort = request.Sort,
                Pagination = request.Pagination
            }, cancellationToken);

        var responseModels = await GenerateResponseModelsAsync(response.List, request.Filter.Language, cancellationToken);
        return new PageResponseDto<PlayerChangesLogResponseDto>(response.Pagination, responseModels);
    }

    /// <summary>
    ///     Generate response models(List of ResponseDto)
    /// </summary>
    /// <param name="domainModels">Collection of PlayerChangesLogDomainModel</param>
    /// <param name="language">Language for localization</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response model for player card logchanges</returns>
    private async Task<IEnumerable<PlayerChangesLogResponseDto>> GenerateResponseModelsAsync(
        IEnumerable<PlayerChangesLogDomainModel> domainModels, string? language, CancellationToken cancellationToken)
    {
        var groupedModels = domainModels.GroupBy(model => model.ModuleName);
        var eventByModules = await _mediator.Send(new GetEventsRequest(), cancellationToken);

        return await groupedModels.SelectManyAsync(
            groupedModel => GenerateResponseGroupedModelsAsync(groupedModel, eventByModules[groupedModel.Key], language, cancellationToken));
    }

    /// <summary>
    ///     Generate response models(List of ResponseDto)
    /// </summary>
    /// <param name="groupedModel">Grouped models by module</param>
    /// <param name="events">Events by module</param>
    /// <param name="language">Language for localization</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Response model for player card logchanges</returns>
    private async Task<IEnumerable<PlayerChangesLogResponseDto>> GenerateResponseGroupedModelsAsync(
        IGrouping<ModuleName, PlayerChangesLogDomainModel> groupedModel, EventDomainModel[] @events, string? language, CancellationToken cancellationToken)
    {
        var cc = groupedModel.GetType();
        var localizedKeys = await LocalizeKeysForGroupedModelsAsync(groupedModel, language, cancellationToken);

        return groupedModel.Select(model => SelectToPlayerChangesLogResponseDto(model, localizedKeys,
            events.FirstOrDefault(@event => @event.Event == model.EventCode)?.Name));
    }

    /// <summary>
    ///     Perform localization on user attribute keys based on a group of models
    /// </summary>
    /// <param name="groupeModels">Grouped models by module</param>
    /// <param name="language">Language for localization</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Directory of localized keys</returns>
    private async Task<IDictionary<string, string>> LocalizeKeysForGroupedModelsAsync(
        IGrouping<ModuleName, PlayerChangesLogDomainModel> groupeModels, string? language, CancellationToken cancellationToken)
    {
        var keysForLocalization =
            groupeModels.SelectMany(model => model.NewValues.Keys.Union(model.OldValues.Keys)).Distinct().ToList();
        return await _localizer.TryLocalize(new LocalizeKeysRequest(groupeModels.Key, language, keysForLocalization),
            cancellationToken);
    }

    /// <summary>
    ///     Select data to response model(player card logchanges)
    /// </summary>
    /// <param name="model">Changelog in player card</param>
    /// <param name="localizedKeys">Directory of localized keys</param>
    /// <param name="eventName">Event name</param>
    /// <returns>Response model for player card logchanges</returns>
    private static PlayerChangesLogResponseDto SelectToPlayerChangesLogResponseDto(PlayerChangesLogDomainModel model,
        IDictionary<string, string> localizedKeys, string? eventName)
        => new()
        {
            UserId = model.User.Id,
            UserLogin = model.User.Email,
            EventKey = model.EventCode,
            EventName = eventName ?? "-",
            IpAddress = model.IpAddress,
            Reason = model.Reason,
            Timestamp = model.Timestamp,
            NewValue = model.NewValues.Select(attribute => LocalizePlayerAttribute(attribute, localizedKeys)),
            OldValue = model.OldValues.Select(attribute => LocalizePlayerAttribute(attribute, localizedKeys))
        };


    /// <summary>
    ///     Localize player attribute
    /// </summary>
    /// <param name="attribute">User attribute</param>
    /// <param name="localizedKeys">Directory of localized keys</param>
    /// <returns>Localized attribute player, reflects changed fields</returns>
    private static LocalizedPlayerAttributeDomainModel LocalizePlayerAttribute(
        KeyValuePair<string, PlayerAttributeDomainModel> attribute, IDictionary<string, string> localizedKeys)
        => new()
        {
            Value = attribute.Value.Value,
            Type = attribute.Value.Type,
            Label = attribute.Value.IsTranslatable ? localizedKeys[attribute.Key] : attribute.Key
        };
}