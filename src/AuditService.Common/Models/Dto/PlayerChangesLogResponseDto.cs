using AuditService.Common.Models.Domain.PlayerChangesLog;
using Newtonsoft.Json;

namespace AuditService.Common.Models.Dto;

/// <summary>
///     Response model for player card logchanges
/// </summary>
public class PlayerChangesLogResponseDto
{
    public PlayerChangesLogResponseDto()
    {
        UserLogin = string.Empty;
        IpAddress = string.Empty;
        EventKey = string.Empty;
        EventName = string.Empty;
        Reason = string.Empty;
        OldValue = new List<LocalizedPlayerAttributeDomainModel>();
        NewValue = new List<LocalizedPlayerAttributeDomainModel>();
    }

    /// <summary>
    ///     Date and time of the event
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     User login
    /// </summary>
    public string UserLogin { get; set; }

    /// <summary>
    ///     User Id
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     IP address of the user making the change
    /// </summary>
    public string IpAddress { get; set; }

    /// <summary>
    ///     Event key
    /// </summary>
    public string EventKey { get; set; }

    /// <summary>
    ///     Event key name
    /// </summary>
    public string EventName { get; set; }

    /// <summary>
    ///     Reason for the change
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    ///     Old user attributes
    /// </summary>
    public IEnumerable<LocalizedPlayerAttributeDomainModel> OldValue { get; set; }

    /// <summary>
    ///     New user attributes
    /// </summary>
    public IEnumerable<LocalizedPlayerAttributeDomainModel> NewValue { get; set; }
}