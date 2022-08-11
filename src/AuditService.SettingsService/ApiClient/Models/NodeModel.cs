﻿namespace AuditService.SettingsService.ApiClient.Models;

/// <summary>
///     Node model(settings service model)
/// </summary>
public class NodeModel
{
    /// <summary>
    ///     Node Id
    /// </summary>
    public long? Id { get; set; }

    /// <summary>
    ///     Node title
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    ///     Node type
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    ///     Node uuid
    /// </summary>
    public Guid? Uuid { get; set; }

    /// <summary>
    ///     Node children
    /// </summary>
    public NodeModel[]? Children { get; set; }
}