using System.Text.Json.Serialization;

namespace AuditService.Data.Domain.Enums;

/// <summary>
///     Identificator of service
/// </summary>
// ReSharper disable InconsistentNaming
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServiceIdentity
{
    /// <summary>
    ///     PAYMENT SERVICE
    /// </summary>
    PAYMENTSERVICE = 0,

    /// <summary>
    ///     SETTING
    /// </summary>
    SETTINGSERVICE = 1,

    /// <summary>
    ///     SSO
    /// </summary>
    SSOSERVICE = 2,

    /// <summary>
    ///     GS
    /// </summary>
    GSSERVICE = 3,

    /// <summary>
    ///     REPORT
    /// </summary>
    REPORTSERVICE = 4,

    /// <summary>
    ///     BO
    /// </summary>
    BOSERVICE = 5,

    /// <summary>
    ///     RM
    /// </summary>
    RMSERVICE = 6,

    /// <summary>
    ///     CONSTRUCTOR
    /// </summary>
    CONSTRUCTORSERVICE = 7
}