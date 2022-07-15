using System.ComponentModel;
using System.Text.Json.Serialization;

namespace AuditService.Common.Enums;

/// <summary>
///     Identificator of service
/// </summary>
// ReSharper disable InconsistentNaming
public enum ServiceStructure
{
    /// <summary>
    ///     PAYMENT SERVICE
    /// </summary>
    [Description("PAYMENT SERVICE")]
    PAY = 0,

    /// <summary>
    ///     SETTING
    /// </summary>
    [Description("SETTING SERVICE")]
    SS  = 1,

    /// <summary>
    ///     SSO
    /// </summary>
    [Description("SSO SERVICE")]
    SSO = 2,

    /// <summary>
    ///     GS
    /// </summary>
    [Description("GAMES SERVICE")]
    GS = 3,

    /// <summary>
    ///     REPORT
    /// </summary>
    [Description("REPORT SERVICE")]
    REP = 4,

    /// <summary>
    ///     BO
    /// </summary>
    [Description("BO SERVICE")]
    BO = 5,

    /// <summary>
    ///     RM
    /// </summary>
    [Description("RISK MANAGEMENT SERVICE")]
    FRAUD = 6,

    /// <summary>
    ///     CONSTRUCTOR
    /// </summary>
    [Description("CONSTRUCTOR SERVICE")]
    CCR = 7,
    
    /// <summary>
    ///     KYC
    /// </summary>
    [Description("KYC SERVICE")]
    KYC = 8,
    
    /// <summary>
    ///     BONUS
    /// </summary>
    [Description("BONUS SERVICE")]
    LAB = 9,
    
    /// <summary>
    ///     TOURNAMENTS
    /// </summary>
    [Description("TOURNAMENTS SERVICE")]
    TS = 10,
    
    /// <summary>
    ///     SEGMENTATION
    /// </summary>
    [Description("SEGMENTATION SERVICE")]
    SEG = 11,
    
    /// <summary>
    ///     BI
    /// </summary>
    [Description("BUSINESS INTELLIGENCE SERVICE")]
    BI = 12,
    
    /// <summary>
    ///     AFFILIATE
    /// </summary>
    [Description("AFFILIATE SERVICE")]
    AFS = 13,
    
    /// <summary>
    ///     BALANCE
    /// </summary>
    [Description("BALANCE SERVICE")]
    BJS = 14
}