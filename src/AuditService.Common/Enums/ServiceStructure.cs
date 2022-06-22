using System.Text.Json.Serialization;

namespace AuditService.Common.Enums;

/// <summary>
///     Identificator of service
/// </summary>
// ReSharper disable InconsistentNaming
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ServiceStructure
{
    /// <summary>
    ///     PAYMENT SERVICE
    /// </summary>
    PAY = 0,

    /// <summary>
    ///     SETTING
    /// </summary>
    SS  = 1,

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
    REP = 4,

    /// <summary>
    ///     BO
    /// </summary>
    BO = 5,

    /// <summary>
    ///     RM
    /// </summary>
    FRAUD = 6,

    /// <summary>
    ///     CONSTRUCTOR
    /// </summary>
    CCR = 7,
    
    /// <summary>
    ///     KYC
    /// </summary>
    KYC = 8,
    
    /// <summary>
    ///     BONUS
    /// </summary>
    LAB = 9,
    
    /// <summary>
    ///     TOURNAMENTS
    /// </summary>
    TS = 10,
    
    /// <summary>
    ///     SEGMENTATION
    /// </summary>
    SEG = 11,
    
    /// <summary>
    ///     BI
    /// </summary>
    BI = 12,
    
    /// <summary>
    ///     AFFILIATE
    /// </summary>
    AFS = 13
}