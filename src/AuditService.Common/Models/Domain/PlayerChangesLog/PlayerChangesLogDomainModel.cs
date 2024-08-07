﻿using System.ComponentModel.DataAnnotations;
using AuditService.Common.Enums;
using AuditService.Common.Models.Interfaces;

namespace AuditService.Common.Models.Domain.PlayerChangesLog;

/// <summary>
///     Changelog in player card
/// </summary>
public class PlayerChangesLogDomainModel : INodeId
{
    public PlayerChangesLogDomainModel()
    {
        EventCode = string.Empty;
        IpAddress = string.Empty;
        ModuleName = string.Empty;
        EventInitiator = string.Empty;
        User = new UserInitiatorDomainModel();
        OldValue = new List<PlayerAttributeDomainModel>();
        NewValue = new List<PlayerAttributeDomainModel>();
    }

    /// <summary>
    ///     Get module name
    /// </summary>
    /// <returns>Module name</returns>
    public ModuleName GetModuleName() => Enum.Parse<ModuleName>(ModuleName);

    /// <summary>
    ///     Enumeration of module names
    /// </summary>
    [Required]
    public string ModuleName { get; set; }

    /// <summary>
    ///     Player's room Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Event name from the table
    /// </summary>
    [Required]
    public string EventCode { get; set; }

    /// <summary>
    ///     Enumeration of event initiator
    /// </summary>
    [Required]
    public string EventInitiator { get; set; }

    /// <summary>
    ///     Date and time of the event
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     The Id of the player by whom the changes were made
    /// </summary>
    [Required]
    public Guid PlayerId { get; set; }

    /// <summary>
    ///     IP address of the user making the change
    /// </summary>
    [Required]
    public string IpAddress { get; set; }

    /// <summary>
    ///     Old user attributes
    /// </summary>
    public List<PlayerAttributeDomainModel> OldValue { get; set; }

    /// <summary>
    ///     New user attributes
    /// </summary>
    public List<PlayerAttributeDomainModel> NewValue { get; set; }

    /// <summary>
    ///     Reason for the change
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    ///     The object of the user who made the changes (event initiator)
    /// </summary>
    [Required]
    public UserInitiatorDomainModel User { get; set; }
}