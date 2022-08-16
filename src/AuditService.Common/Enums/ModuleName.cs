using System.ComponentModel;
using AuditService.Common.Attributes;

namespace AuditService.Common.Enums;

/// <summary>
///     Identificator of service
/// </summary>
// ReSharper disable InconsistentNaming
public enum ModuleName
{
    /// <summary>
    ///     PAYMENT SERVICE
    /// </summary>
    [Description("PAYMENT SERVICE")]
    [Localization(Key = "payment-back")]
    PAY = 0,

    /// <summary>
    ///     SETTING
    /// </summary>
    [Description("SETTING SERVICE")]
    [Localization(Key = "settings-back")]
    SS  = 1,

    /// <summary>
    ///     SSO
    /// </summary>
    [Description("SSO SERVICE")]
    [Localization(Key = "sso-back")]
    SSO = 2,

    /// <summary>
    ///     GS
    /// </summary>
    [Description("GAMES SERVICE")]
    [Localization(Key = "games-back")]
    GS = 3,

    /// <summary>
    ///     REPORT
    /// </summary>
    [Description("REPORT SERVICE")]
    [Localization(Key = "reports_backend_api-back")]
    REP = 4,

    /// <summary>
    ///     BO
    /// </summary>
    [Description("BO SERVICE")]
    //[Localization(Key = "bonuses-back")]
    BO = 5,

    /// <summary>
    ///     RM
    /// </summary>
    [Description("RISK MANAGEMENT SERVICE")]
    [Localization(Key = "antifraud-back")]
    FRAUD = 6,

    /// <summary>
    ///     CONSTRUCTOR
    /// </summary>
    [Description("CONSTRUCTOR SERVICE")]
    [Localization(Key = "constructor-backend")]
    CCR = 7,
    
    /// <summary>
    ///     KYC
    /// </summary>
    [Description("KYC SERVICE")]
    [Localization(Key = "kyc-back")]
    KYC = 8,
    
    /// <summary>
    ///     BONUS
    /// </summary>
    [Description("BONUS SERVICE")]
    [Localization(Key = "bonuses-back")]
    LAB = 9,
    
    /// <summary>
    ///     TOURNAMENTS
    /// </summary>
    [Description("TOURNAMENTS SERVICE")]
    [Localization(Key = "tournament-back")]
    TS = 10,
    
    /// <summary>
    ///     SEGMENTATION
    /// </summary>
    [Description("SEGMENTATION SERVICE")]
    [Localization(Key = "segmentation-back")]
    SEG = 11,
    
    /// <summary>
    ///     BI
    /// </summary>
    [Description("BUSINESS INTELLIGENCE SERVICE")]
    [Localization(Key = "business_intelligence-back")]
    BI = 12,
    
    /// <summary>
    ///     AFFILIATE
    /// </summary>
    [Description("AFFILIATE SERVICE")]
    [Localization(Key = "affiliate-api")]
    AFS = 13,
    
    /// <summary>
    ///     BALANCE
    /// </summary>
    [Description("BALANCE SERVICE")]
    [Localization(Key = "balance-back")]
    BJS = 14,
    
    
    /// <summary>
    ///     RESPONSIBLE GAMING
    /// </summary>
    [Description("RESPONSIBLE GAMING SERVICE")]
    [Localization(Key = "balance-back")]
    RG = 15
}