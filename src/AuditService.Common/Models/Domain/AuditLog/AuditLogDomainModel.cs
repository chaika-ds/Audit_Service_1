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
        User = new AuditLogUserDomainModel();
        ActionName = string.Empty;
        ModuleName = string.Empty;
        OldValue = new List<AuditLogAttributeDomainModel>();
        NewValue = new List<AuditLogAttributeDomainModel>();
        RequestBody = new List<AuditLogAttributeDomainModel>();
    }

    /// <summary>
    ///     Get module name
    /// </summary>
    /// <returns>Module name</returns>
    public ModuleName GetModuleName() => Enum.Parse<ModuleName>(ModuleName);

    /// <summary>
    ///     Module Name
    /// </summary>
    [Required]
    public string ModuleName { get; set; }

    /// <summary>
    ///     ID of the node where the change occurred
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Type of action
    /// </summary>
    [Required]
    public string ActionName { get; set; }

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
    public List<AuditLogAttributeDomainModel> RequestBody { get; set; }

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
    public List<AuditLogAttributeDomainModel> OldValue { get; set; }

    /// <summary>
    ///     JSON representation of a new entity value
    /// </summary>
    public List<AuditLogAttributeDomainModel> NewValue { get; set; }

    /// <summary>
    ///     User
    /// </summary>
    [Required]
    public AuditLogUserDomainModel User { get; set; }
}