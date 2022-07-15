using AuditService.Common.Models.Domain.BlockedPlayersLog;
using AuditService.Common.Models.Dto;

namespace AuditService.Handlers.Mappers;

/// <summary>
///     Blocked player log model mapper
/// </summary>
public static class BlockedPlayersLogDomainModelMapper
{
    /// <summary>
    ///     Perform mapping to the DTO model
    /// </summary>
    /// <param name="model">Log of blocked players(Domain model)</param>
    /// <returns>Blocked player log response model</returns>
    public static BlockedPlayersLogResponseDto MapToBlockedPlayersLogResponseDto(this BlockedPlayersLogDomainModel model)
     => new()
     {
         BlockingDate = model.BlockingDate,
         PreviousBlockingDate = model.PreviousBlockingDate,
         PlayerLogin = model.PlayerLogin,
         PlayerId = model.PlayerId,
         BlocksCounter = model.BlocksCounter,
         Browser = model.Browser,
         HallId = model.HallId,
         Language = model.Language,
         OperatingSystem = model.Platform,
         PlayerIp = model.LastVisitIpAddress,
         Timestamp = model.Timestamp,
         BrowserVersion = model.BrowserVersion
     };
}