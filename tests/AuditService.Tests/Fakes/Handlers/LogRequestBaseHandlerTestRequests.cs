using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Pagination;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.TRASH;

namespace AuditService.Tests.Fakes.Handlers;

/// <summary>
/// Responses for checking PlayerChangesLogRequestHandler methods responses data
/// </summary>
internal class LogRequestBaseHandlerTestRequests
{
    /// <summary>
    /// Test data for LogFilterRequestDto request param input to Handler
    /// </summary>
    /// <returns>Test data for LogFilterRequestDto</returns>
    internal static LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto>
        GetTestLogFilterRequest()
    {
        return
            new LogFilterRequestDto<PlayerChangesLogFilterDto, LogSortDto, PlayerChangesLogResponseDto>
            {
                Filter = GetTestPlayerChangesLogFilterDto(),
                Pagination = new PaginationRequestDto(),
                Sort = new LogSortDto()
                {
                    SortableType = SortableType.Ascending
                }
            };
    }

    /// <summary>
    /// Test data for PlayerChangesLogFilterDto request input to Handler
    /// </summary>
    /// <returns>PlayerChangesLogFilterDto</returns>
    internal static PlayerChangesLogFilterDto GetTestPlayerChangesLogFilterDto()
    {
        return new PlayerChangesLogFilterDto
        {
            Login = FakeValues.UserLogin1,
            IpAddress = FakeValues.IpAddress1,
            TimestampFrom = FakeValues.Timestamp1,
            TimestampTo = FakeValues.Timestamp2,
            EventKeys = new List<string>()
            {
                "EventKey1",
                "EventKey2"
            },
            Language = FakeValues.LanguageTest
        };
    }

    /// <summary>
    /// Check request for GenerateResponseModelsAsync method
    /// </summary>
    /// <returns>PlayerChangesLogResponseDto array</returns>
    internal static IEnumerable<PlayerChangesLogResponseDto> GetPlayerChangesLogResponseDtoTestRequest()
    {
        return new List<PlayerChangesLogResponseDto>()
        {
            new ()
            {
                EventKey = FakeValues.EventKey1,
                EventName = FakeValues.EventName1,
                IpAddress = FakeValues.IpAddress1,
                Timestamp = FakeValues.Timestamp1,
                Reason = FakeValues.Reason1,
                UserId = FakeValues.UserId1,
                UserLogin = FakeValues.UserLogin1,
                NewValue = new List<LocalizedPlayerAttributeDomainModel>()
                {
                    new()
                    {
                        Label = FakeValues.LocalizeValue1,
                        Type = FakeValues.Type1,
                        Value = FakeValues.Value1
                    },
                    new ()
                    {
                        Label = "TestKey2",
                        Type = FakeValues.Type2,
                        Value = FakeValues.Value2
                    }
                },
                OldValue = new List<LocalizedPlayerAttributeDomainModel>()
                {
                    new()
                    {
                        Label = FakeValues.LocalizeValue3,
                        Type = FakeValues.Type3,
                        Value = FakeValues.Value3
                    },
                    new()
                    {
                        Label = FakeValues.LocalizeValue4,
                        Type = FakeValues.Type4,
                        Value = FakeValues.Value4
                    }
                }
            },
            new ()
            {
                EventKey = FakeValues.EventKey2,
                EventName = FakeValues.EventName2,
                IpAddress = FakeValues.IpAddress2,
                Timestamp = FakeValues.Timestamp2,
                Reason = FakeValues.Reason2,
                UserId = FakeValues.UserId2,
                UserLogin = FakeValues.UserLogin2,
                NewValue = new List<LocalizedPlayerAttributeDomainModel>()
                {
                    new()
                    {
                        Label = FakeValues.LocalizeValue5,
                        Type = FakeValues.Type5,
                        Value = FakeValues.Value5
                    },
                    new ()
                    {
                        Label = "TestKey6",
                        Type = FakeValues.Type6,
                        Value = FakeValues.Value6
                    }
                },
                OldValue = new List<LocalizedPlayerAttributeDomainModel>()
                {
                    new()
                    {
                        Label = FakeValues.LocalizeValue7,
                        Type = FakeValues.Type7,
                        Value = FakeValues.Value7
                    },
                    new()
                    {
                        Label = FakeValues.LocalizeValue8,
                        Type = FakeValues.Type8,
                        Value = FakeValues.Value8
                    }
                }
            }
        };
    }

    /// <summary>
    /// Check request for GenerateResponseGroupedModelsAsync method
    /// </summary>
    /// <returns>PlayerChangesLogResponseDto array</returns>
    internal static IEnumerable<PlayerChangesLogResponseDto> GetPlayerChangesLogResponseGroupedDtoTestRequest()
    {
        return new List<PlayerChangesLogResponseDto>()
        {
            new ()
            {
                EventKey = FakeValues.EventKey1,
                EventName = FakeValues.EventName1,
                IpAddress = FakeValues.IpAddress1,
                Timestamp = FakeValues.Timestamp1,
                Reason = FakeValues.Reason1,
                UserId = FakeValues.UserId1,
                UserLogin = FakeValues.UserLogin1,
                NewValue = new List<LocalizedPlayerAttributeDomainModel>()
                {
                    new()
                    {
                        Label = FakeValues.LocalizeValue1,
                        Type = FakeValues.Type1,
                        Value = FakeValues.Value1
                    },
                    new ()
                    {
                        Label = "TestKey2",
                        Type = FakeValues.Type2,
                        Value = FakeValues.Value2
                    }
                },
                OldValue = new List<LocalizedPlayerAttributeDomainModel>()
                {
                    new()
                    {
                        Label = FakeValues.LocalizeValue3,
                        Type = FakeValues.Type3,
                        Value = FakeValues.Value3
                    },
                    new()
                    {
                        Label = FakeValues.LocalizeValue4,
                        Type = FakeValues.Type4,
                        Value = FakeValues.Value4
                    }
                }
            }
        };
    }
}