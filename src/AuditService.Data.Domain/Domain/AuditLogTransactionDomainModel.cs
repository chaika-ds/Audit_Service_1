﻿using System.ComponentModel.DataAnnotations;
using AuditService.Data.Domain.Enums;

namespace AuditService.Data.Domain.Domain;

/// <summary>
///     Audit log transaction
/// </summary>
public class AuditLogTransactionDomainModel
{
    public AuditLogTransactionDomainModel()
    {
        User = new IdentityUserDomainModel();
    }

    /// <summary>
    ///     Service ID
    /// </summary>
    [Required]
    public ServiceId Service { get; set; }

    /// <summary>
    ///     ID of the node where the change occurred
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Node Type
    /// </summary>
    [Required]
    public NodeType Node { get; set; }

    /// <summary>
    ///     Type of action
    /// </summary>
    [Required]
    public ActionType Action { get; set; }

    /// <summary>
    ///     Category of actions (depending on modules)
    /// </summary>
    [Required]
    public string CategoryCode { get; set; }

    /// <summary>
    ///     The text representation of the request
    /// </summary>
    public string RequestUrl { get; set; }

    /// <summary>
    ///     The JSON representation of the request
    /// </summary>
    public string RequestBody { get; set; }

    /// <summary>
    ///     Date and time of the event (ISO 8601 UTC standard)
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     The name of the class (or table in the database) of the logged entity
    /// </summary>
    public string EntityName { get; set; }

    /// <summary>
    ///     ID of the logged entity (possible types of UUID/Long values)
    /// </summary>
    public Guid EntityId { get; set; }

    /// <summary>
    ///     JSON representation of the previous value of the entity
    /// </summary>
    public string OldValue { get; set; }

    /// <summary>
    ///     JSON representation of a new entity value
    /// </summary>
    public string NewValue { get; set; }

    /// <summary>
    ///     ID of the project from which the change is logged in the audit
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     User
    /// </summary>
    public IdentityUserDomainModel User { get; set; }
}