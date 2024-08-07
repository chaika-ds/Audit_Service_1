﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.Common.Models.Dto.Filter;

/// <summary>
///     Blocked players log filter model
/// </summary>
public class BlockedPlayersLogFilterDto : ILogFilter
{
    /// <summary>
    ///     Login of the blocked player
    ///     (if not - then mail, if there is no mail - phone)
    /// </summary>
    public string? PlayerLogin { get; set; }

    /// <summary>
    ///     Blocked player Id
    /// </summary>
    public Guid? PlayerId { get; set; }

    /// <summary>
    ///     The IP address from which the login attempt was made before being blocked
    /// </summary>
    public string? PlayerIp { get; set; }

    /// <summary>
    ///     Player node Id
    /// </summary>
    public Guid? NodeId { get; set; }

    /// <summary>
    ///     Date and time of the previous block(Start date)
    /// </summary>
    public DateTime? PreviousBlockingDateFrom { get; set; }

    /// <summary>
    ///     Date and time of the previous block(End date)
    /// </summary>
    public DateTime? PreviousBlockingDateTo { get; set; }

    /// <summary>
    ///     The operating system of the device with which the player logged in
    /// </summary>
    public string? Platform { get; set; }

    /// <summary>
    ///     Browser that the player logged in from
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    ///     Browser version
    /// </summary>
    public string? Version { get; set; }

    /// <summary>
    ///     Player interface language code
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    ///      Date and time the lock was set(Start date)
    /// </summary>
    [Required]
    [ModelBinder(Name = "blockingDateFrom")]
    public DateTime TimestampFrom { get; set; }

    /// <summary>
    ///      Date and time the lock was set(End date)
    /// </summary>
    [Required]
    [ModelBinder(Name = "blockingDateTo")]
    public DateTime TimestampTo { get; set; }
}