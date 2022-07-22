using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Pagination;

namespace AuditService.Tests.AuditService.Handlers;

/// <summary>
/// Fakes requests for methods in PlayerChangesLogRequestHandler mocked interfaces
/// </summary>
internal class ResponsesFake
{
    /// <summary>
    /// Fake request for mediator Send method with LogFilterRequestDto param
    /// </summary>
    /// <returns>PageResponseDto of PlayerChangesLogDomainModel</returns>
    internal PageResponseDto<PlayerChangesLogDomainModel> GetSendHandleResponse()
    {
        return new PageResponseDto<PlayerChangesLogDomainModel>(
            new PaginationResponseDto(1, 1, 1),
            new List<PlayerChangesLogDomainModel>
            {
                new()
                {
                    User = new()
                    {
                        Email = "test@test.email",
                        Id = Guid.NewGuid(),
                        UserAgent = "TestAgent"
                    },
                    EventCode = "TestEventCode1",
                    IpAddress = "000.000.000.000",
                    Reason = "TestReason",
                    Timestamp = DateTime.Today.AddDays(-1),
                    NewValues = new Dictionary<string, PlayerAttributeDomainModel>()
                    {
                        ["TestKey1"] = new()
                        {
                            Type = "UserAttributeType1",
                            Value = "UserAttributeValue1",
                            IsTranslatable = true
                        },
                        ["TestKey2"] = new()
                        {
                            Type = "UserAttributeType2",
                            Value = "UserAttributeValue2"
                        }

                    },
                    OldValues = new Dictionary<string, PlayerAttributeDomainModel>()
                    {
                        ["TestKey3"] = new()
                        {
                            Type = "UserAttributeType3",
                            Value = "UserAttributeValue3",
                            IsTranslatable = true
                        },
                        ["TestKey4"] = new()
                        {
                            Type = "UserAttributeType4",
                            Value = "UserAttributeValue4",
                            IsTranslatable = true
                        }
                    },
                    ModuleName = ModuleName.BI
                }
            });
    }

    /// <summary>
    /// Fake request for mediator Send method with GetEventsRequest param
    /// </summary>
    /// <returns>Event by Modules of EventDomainModel</returns>
    public IDictionary<ModuleName, EventDomainModel[]> GetSendResponseModelsResponse()
    {
        return new Dictionary<ModuleName, EventDomainModel[]>()
        {
            [ModuleName.BI] = new[]
            {
                new EventDomainModel
                {
                    Event = "TestEventCode1",
                    Name = "TestEventName1"
                }
            },
            [ModuleName.SS] = new[]
            {
                new EventDomainModel
                {
                    Event = "TestEventCode2",
                    Name = "TestEventName2"
                }
            }
        };
    }

    /// <summary>
    /// Fake request for Localizator TryLocalize method
    /// </summary>
    /// <returns>Directory of localized keys</returns>
    internal IDictionary<string, string> GetTestTryLocalizeResponse()
    {
        var resources = new Dictionary<string, string>()
        {
            ["TestKey1"] = "LocalizeValue1",
            ["TestKey2"] = "LocalizeValue2",
            ["TestKey3"] = "LocalizeValue3",
            ["TestKey4"] = "LocalizeValue4"
        };
        return resources;
    }
}