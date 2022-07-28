﻿using System.ComponentModel.DataAnnotations;
using AuditService.Common.Enums;

namespace AuditService.Common.Models.Domain;

/// <summary>
///     An object containing authorization data
/// </summary>
public class AuthorizationDataDomainModel
{
    public AuthorizationDataDomainModel()
    {
        OperatingSystem = string.Empty;
        Browser = string.Empty;
    }

    /// <summary>
    ///     Device type
    /// </summary>
    [Required]
    public DeviceType DeviceType { get; set; }

    /// <summary>
    ///     Operating system
    /// </summary>
    [Required]
    public string OperatingSystem { get; set; }

    /// <summary>
    ///     The name of the browser from which authorization was performed
    /// </summary>
    [Required]
    public string Browser { get; set; }

    /// <summary>
    ///     Player authorization type
    /// </summary>
    public string? AuthorizationType { get; set; }
}