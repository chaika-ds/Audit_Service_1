using System.Text.Json.Serialization;

namespace AuditService.Data.Domain.Enums;

/// <summary>
///     Identificator of service
/// </summary>
// ReSharper disable InconsistentNaming
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServiceId
{
    /// <summary>
    ///     PAYMENT SERVICE
    /// </summary>
    PAYMENT = 0,

    /// <summary>
    ///     SETTING
    /// </summary>
    SETTING = 1,

    /// <summary>
    ///     SSO
    /// </summary>
    SSO = 2,

    /// <summary>
    ///     GS
    /// </summary>
    GS = 3,

    /// <summary>
    ///     REPORT
    /// </summary>
    REPORT = 4,

    /// <summary>
    ///     BO
    /// </summary>
    BO = 5,

    /// <summary>
    ///     RM
    /// </summary>
    RM = 6,

    /// <summary>
    ///     CONSTRUCTOR
    /// </summary>
    CONSTRUCTOR = 7
}