﻿using System.ComponentModel.DataAnnotations;
using AuditService.Common.Enums;
using AuditService.Common.Models.Interfaces;

namespace AuditService.Common.Models.Domain.AuditLog;

/// <summary>
///     Audit log domain model
/// </summary>
public class AuditLogDomainModel : INodeId
{
    public AuditLogDomainModel()
    {
        CategoryCode = string.Empty;
        User = new IdentityUserDomainModel();
    }

    /// <summary>
    ///     Module Name
    /// </summary>
    [Required]
    public ModuleName ModuleName { get; set; }

    /// <summary>
    ///     ID of the node where the change occurred
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Type of action
    /// </summary>
    [Required]
    public ActionType ActionName { get; set; }

    /// <summary>
    ///     Category of actions (depending on modules)
    /// </summary>
    [Required]
    public string CategoryCode { get; set; }

    /// <summary>
    ///     The text representation of the request
    /// </summary>
    public string? RequestUrl { get; set; }

    /// <summary>
    ///     The JSON representation of the request
    /// </summary>
    public string? RequestBody { get; set; }

    /// <summary>
    ///     Date and time of the event (ISO 8601 UTC standard)
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     The name of the class (or table in the database) of the logged entity
    /// </summary>
    public string? EntityName { get; set; }

    /// <summary>
    ///     ID of the logged entity (possible types of UUID/Long values)
    /// </summary>
    public string? EntityId { get; set; }

    /// <summary>
    ///     JSON representation of the previous value of the entity
    /// </summary>
    public string? OldValue { get; set; }

    /// <summary>
    ///     JSON representation of a new entity value
    /// </summary>
    public string? NewValue { get; set; }

    /// <summary>
    ///     User
    /// </summary>
    [Required]
    public IdentityUserDomainModel User { get; set; }
}