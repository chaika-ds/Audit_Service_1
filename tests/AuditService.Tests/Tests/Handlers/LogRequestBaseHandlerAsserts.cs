using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Pagination;

namespace AuditService.Tests.Tests.Handlers;

/// <summary>
///     Assert for testing LogRequestBaseHandler
/// </summary>
public static class LogRequestBaseHandlerAsserts
{
    /// <summary>
    ///     Assert if pagination mapping is correct
    /// </summary>
    /// <param name="paginationResponse">PaginationResponseDto</param>
    /// <param name="paginationMock">PaginationResponseDto</param>
    public static void IsEqualPaginationResponse(PaginationResponseDto paginationResponse,
        PaginationResponseDto paginationMock)
    {
        Equal(paginationResponse.Total, paginationMock.Total);
        Equal(paginationResponse.PageNumber, paginationMock.PageNumber);
        Equal(paginationResponse.PageSize, paginationMock.PageSize);
    }

    /// <summary>
    ///     Assert if items of PlayerChangesLogDomainModel mapped correctly
    /// </summary>
    /// <param name="playerChangesLogResponse">Array of PlayerChangesLogResponseDto</param>
    /// <param name="playerChangesLogMock">Array of mocked PlayerChangesLogDomainModel</param>
    /// <param name="eventName">Event names array</param>
    public static void IsEqualPlayerChangesLogResponse(List<PlayerChangesLogResponseDto> playerChangesLogResponse,
        List<PlayerChangesLogDomainModel> playerChangesLogMock, string[] eventName)
    {
        playerChangesLogResponse.ForEach(log =>
        {
            Contains(log.UserId, playerChangesLogMock.Select(x => x.User.Id));
            Contains(log.UserLogin, playerChangesLogMock.Select(x => x.User.Email));
            Contains(log.EventKey, playerChangesLogMock.Select(x => x.EventCode));
            Contains(log.EventName, eventName);
            Contains(log.IpAddress, playerChangesLogMock.Select(x => x.IpAddress));
            Contains(log.Reason, playerChangesLogMock.Select(x => x.Reason));
            Contains(log.Timestamp, playerChangesLogMock.Select(x => x.Timestamp));
        });
    }

    /// <summary>
    ///     Assert if localize player attribute mapped correctly
    /// </summary>
    /// <param name="playerChangesLogResponse">Array of PlayerChangesLogResponseDto</param>
    /// <param name="playerChangesLogMock">Array of mocked PlayerChangesLogDomainModel</param>
    /// <param name="localizedKeys">Directory of localized keys</param>
    public static void IsLocalizePlayerAttributeLocalized(List<PlayerChangesLogResponseDto> playerChangesLogResponse,
        List<PlayerChangesLogDomainModel> playerChangesLogMock, IDictionary<string, string> localizedKeys)
    {
        playerChangesLogResponse.ForEach(log =>
        {
            log.NewValue.ToList().ForEach(nVal =>
            {
                Contains(nVal.Value, playerChangesLogMock.SelectMany(l => l.NewValue.Select(x => x.Value.Value)));
                Contains(nVal.Type, playerChangesLogMock.SelectMany(l => l.NewValue.Select(x => x.Value.Type)));

                var mockNewValues = playerChangesLogMock.SelectMany(l => l.NewValue.Where(t => t.Value.Value == nVal.Value)).FirstOrDefault();

                Equal(nVal.Label, mockNewValues.Value.IsTranslatable ? localizedKeys[mockNewValues.Key] : mockNewValues.Key);
            });

            log.OldValue.ToList().ForEach(oVal =>
            {
                Contains(oVal.Value, playerChangesLogMock.SelectMany(l => l.OldValue.Select(x => x.Value.Value)));
                Contains(oVal.Type, playerChangesLogMock.SelectMany(l => l.OldValue.Select(x => x.Value.Type)));

                var mockOldValues = playerChangesLogMock.SelectMany(l => l.OldValue.Where(t => t.Value.Value == oVal.Value)).FirstOrDefault();

                Equal(oVal.Label, mockOldValues.Value.IsTranslatable ? localizedKeys[mockOldValues.Key] : mockOldValues.Key);
            });
        });
    }
}