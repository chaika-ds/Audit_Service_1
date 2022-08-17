using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Tests.TRASH;

namespace AuditService.Tests.Fakes.Handlers;

/// <summary>
/// Fakes requests for methods in PlayerChangesLogRequestHandler mocked interfaces
/// </summary>
internal class LogRequestBaseHandlerResponsesFake
{
    /// <summary>
    /// Fake request for mediator Send method with LogFilterRequestDto param
    /// </summary>
    /// <returns>PageResponseDto of PlayerChangesLogDomainModel</returns>
    internal static PageResponseDto<PlayerChangesLogDomainModel> GetSendHandleResponse()
    {
        return new PageResponseDto<PlayerChangesLogDomainModel>(
            new PaginationResponseDto(1, 1, 1, 1),
            GetTestPlayerChangesLogDomainModel());
    }

    /// <summary>
    /// Fake request for mediator Send method with LogFilterRequestDto param
    /// </summary>
    /// <returns>PageResponseDto of PlayerChangesLogDomainModel</returns>
    internal static PlayerChangesLogDomainModel GetTestPlayerChangesLogDomainModelResponse()
    {
        return
            new()
            {
                User = new()
                {
                    Email = FakeValues.UserLogin2,
                    Id = FakeValues.UserId2,
                    UserAgent = FakeValues.UserAgent2
                },
                EventCode = FakeValues.EventCode2,
                IpAddress = FakeValues.IpAddress2,
                Reason = FakeValues.Reason2,
                Timestamp = FakeValues.Timestamp2,
                NewValue = new Dictionary<string, PlayerAttributeDomainModel>()
                {
                    ["TestKey5"] = new()
                    {
                        Type = FakeValues.Type5,
                        Value = FakeValues.Value5,
                        IsTranslatable = true
                    },
                    ["TestKey6"] = new()
                    {
                        Type = FakeValues.Type6,
                        Value = FakeValues.Value6
                    }

                },
                OldValue = new Dictionary<string, PlayerAttributeDomainModel>()
                {
                    ["TestKey7"] = new()
                    {
                        Type = FakeValues.Type7,
                        Value = FakeValues.Value7,
                        IsTranslatable = true
                    },
                    ["TestKey8"] = new()
                    {
                        Type = FakeValues.Type8,
                        Value = FakeValues.Value8,
                        IsTranslatable = true
                    }
                },
                ModuleName = ModuleName.CCR.ToString()
            };
    }

    /// <summary>
    /// Fake request for mediator Send method with array of LogFilterRequestDto param
    /// </summary>
    /// <returns>PageResponseDto of PlayerChangesLogDomainModel</returns>
    internal static List<PlayerChangesLogDomainModel> GetTestPlayerChangesLogDomainModelNullResponse()
    {
        return new List<PlayerChangesLogDomainModel>
        {
            GetTestPlayerChangesLogDomainModelResponse()
        };
    }

    /// <summary>
    /// Fake request for mediator Send method with LogFilterRequestDto param
    /// </summary>
    /// <returns>PageResponseDto of PlayerChangesLogDomainModel</returns>
    internal static List<PlayerChangesLogDomainModel> GetTestPlayerChangesLogDomainModel()
    {
        return new List<PlayerChangesLogDomainModel>
        {
            new()
            {
                User = new()
                {
                    Email = FakeValues.UserLogin1,
                    Id = FakeValues.UserId1,
                    UserAgent = FakeValues.UserAgent1
                },
                EventCode = FakeValues.EventCode1,
                IpAddress = FakeValues.IpAddress1,
                Reason = FakeValues.Reason1,
                Timestamp = FakeValues.Timestamp1,
                NewValue = new Dictionary<string, PlayerAttributeDomainModel>()
                {
                    ["TestKey1"] = new()
                    {
                        Type = FakeValues.Type1,
                        Value = FakeValues.Value1,
                        IsTranslatable = true
                    },
                    ["TestKey2"] = new()
                    {
                        Type = FakeValues.Type2,
                        Value = FakeValues.Value2
                    }

                },
                OldValue = new Dictionary<string, PlayerAttributeDomainModel>()
                {
                    ["TestKey3"] = new()
                    {
                        Type = FakeValues.Type3,
                        Value = FakeValues.Value3,
                        IsTranslatable = true
                    },
                    ["TestKey4"] = new()
                    {
                        Type = FakeValues.Type4,
                        Value = FakeValues.Value4,
                        IsTranslatable = true
                    }
                },
                ModuleName = ModuleName.BI.ToString()
            },
            new()
            {
                User = new()
                {
                    Email = FakeValues.UserLogin2,
                    Id = FakeValues.UserId2,
                    UserAgent = FakeValues.UserAgent2
                },
                EventCode = FakeValues.EventCode2,
                IpAddress = FakeValues.IpAddress2,
                Reason = FakeValues.Reason2,
                Timestamp = FakeValues.Timestamp2,
                NewValue = new Dictionary<string, PlayerAttributeDomainModel>()
                {
                    ["TestKey5"] = new()
                    {
                        Type = FakeValues.Type5,
                        Value = FakeValues.Value5,
                        IsTranslatable = true
                    },
                    ["TestKey6"] = new()
                    {
                        Type = FakeValues.Type6,
                        Value = FakeValues.Value6
                    }

                },
                OldValue = new Dictionary<string, PlayerAttributeDomainModel>()
                {
                    ["TestKey7"] = new()
                    {
                        Type = FakeValues.Type7,
                        Value = FakeValues.Value7,
                        IsTranslatable = true
                    },
                    ["TestKey8"] = new()
                    {
                        Type = FakeValues.Type8,
                        Value = FakeValues.Value8,
                        IsTranslatable = true
                    }
                },
                ModuleName = ModuleName.CCR.ToString()
            }
        };
    }

    /// <summary>
    /// Get EventDomainModels for Invoke GenerateResponseGroupedModelsAsync method
    /// </summary>
    /// <returns>EventDomainModel array</returns>
    internal static EventDomainModel[] GetTestEventDomainModelArray()
    {
        return new[]
        {
            new EventDomainModel()
            {
                Event = FakeValues.EventKey1,
                Name = FakeValues.EventName1
            }
        };
    }

    /// <summary>
    /// Fake request for mediator Send method with GetEventsRequest param
    /// </summary>
    /// <returns>Event by Modules of EventDomainModel</returns>
    internal static IDictionary<ModuleName, EventDomainModel[]> GetSendResponseModelsResponse()
    {
        return new Dictionary<ModuleName, EventDomainModel[]>()
        {
            [ModuleName.BI] = new[]
            {
                new EventDomainModel
                {
                    Event = FakeValues.EventKey1,
                    Name = FakeValues.EventName1
                }
            },
            [ModuleName.CCR] = new[]
            {
                new EventDomainModel
                {
                    Event = FakeValues.EventKey2,
                    Name = FakeValues.EventName2
                }
            }
        };
    }

    /// <summary>
    /// Fake request for mediator Send method with no coincidence key
    /// </summary>
    /// <returns>Event by Modules of EventDomainModel</returns>
    internal static IDictionary<ModuleName, EventDomainModel[]> GetSendNoConcidenceKeyResponse()
    {
        return new Dictionary<ModuleName, EventDomainModel[]>()
        {
            [ModuleName.BI] = new[]
            {
                new EventDomainModel
                {
                    Event = FakeValues.EventKey1,
                    Name = FakeValues.EventName1
                }
            }
        };
    }

    /// <summary>
    /// Fake request for Localizator TryLocalize method
    /// </summary>
    /// <returns>Directory of localized keys</returns>
    internal static IDictionary<string, string> GetTestTryLocalizeResponse()
    {
        var resources = new Dictionary<string, string>()
        {
            ["TestKey1"] = FakeValues.LocalizeValue1,
            ["TestKey2"] = FakeValues.LocalizeValue2,
            ["TestKey3"] = FakeValues.LocalizeValue3,
            ["TestKey4"] = FakeValues.LocalizeValue4,
            ["TestKey5"] = FakeValues.LocalizeValue5,
            ["TestKey6"] = FakeValues.LocalizeValue6,
            ["TestKey7"] = FakeValues.LocalizeValue7,
            ["TestKey8"] = FakeValues.LocalizeValue8
        };
        return resources;
    }

    /// <summary>
    /// Fake request for Localizator TryLocalize method - not all Key
    /// </summary>
    /// <returns>Directory of localized keys</returns>
    internal static IDictionary<string, string> GetTestTryLocalizeResponseNotAllKey()
    {
        var resources = new Dictionary<string, string>()
        {
            ["TestKey1"] = FakeValues.LocalizeValue1,
            ["TestKey2"] = FakeValues.LocalizeValue2,
            ["TestKey3"] = FakeValues.LocalizeValue3,
            ["TestKey4"] = FakeValues.LocalizeValue4,
        };
        return resources;
    }
}