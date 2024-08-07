﻿using System.ComponentModel.DataAnnotations;
using AuditService.Common.Enums;
using AuditService.Common.Models.Interfaces;

namespace AuditService.Common.Models.Domain.VisitLog;

/// <summary>
///     The base model of the visit log
/// </summary>
public abstract class BaseVisitLogDomainModel : INodeId
{
    protected BaseVisitLogDomainModel()
    {
        Login = string.Empty;
        Ip = string.Empty;
        Type = string.Empty;
        Authorization = new AuthorizationDataDomainModel();
    }

    /// <summary>
    ///     NodeId Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Type of the log of visits
    /// </summary>
    [Required]
    public string Type { get; set; }

    /// <summary>
    ///     Ge visit log type
    /// </summary>
    /// <returns>Visit log type</returns>
    public VisitLogType? GeVisitLogType() => Enum.TryParse<VisitLogType>(Type, out var type) ? type : null;

    /// <summary>
    ///     Login
    /// </summary>
    [Required]
    public string Login { get; set; }

    /// <summary>
    ///     IP address
    /// </summary>
    [Required]
    public string Ip { get; set; }

    /// <summary>
    ///     An object containing authorization data
    /// </summary>
    [Required]
    public AuthorizationDataDomainModel Authorization { get; set; }

    /// <summary>
    ///     Date and time of the event
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }
}