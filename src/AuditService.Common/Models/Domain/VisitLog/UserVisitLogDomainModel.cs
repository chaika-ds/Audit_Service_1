﻿using System.ComponentModel.DataAnnotations;
using AuditService.Common.Enums;

namespace AuditService.Common.Models.Domain.VisitLog;

/// <summary>
///     User visit log
/// </summary>
public class UserVisitLogDomainModel : BaseVisitLogDomainModel
{
    public UserVisitLogDomainModel()
    {
        UserRoles = new List<UserRoleDomainModel>();
    }

    /// <summary>
    ///     Node Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Node type
    /// </summary>
    [Required]
    public NodeType NodeType { get; set; }

    /// <summary>
    ///     User Id
    /// </summary>
    [Required]
    public Guid UserId { get; set; }

    /// <summary>
    ///     List of user roles
    /// </summary>
    [Required]
    public List<UserRoleDomainModel> UserRoles { get; set; }
}