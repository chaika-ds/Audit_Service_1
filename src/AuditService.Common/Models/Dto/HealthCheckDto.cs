using Microsoft.AspNetCore.Mvc;

namespace AuditService.Common.Models.Dto;

/// <summary>
///     Checking Health of Services
/// </summary>
public class HealthCheckDto
{
    /// <summary>
    ///     Is Kafka healthy
    /// </summary>
    public bool Kafka { get; set; }

    /// <summary>
    ///     Is Elk healthy
    /// </summary>
    public bool Elk { get; set; }

    /// <summary>
    ///     Is Redis healthy
    /// </summary>
    public bool Redis { get; set; }

    /// <summary>
    ///     Healthy is successed
    /// </summary>
    [ApiExplorerSettings(IgnoreApi = true)]
    public bool IsSuccess() => Kafka && Elk && Redis;
}