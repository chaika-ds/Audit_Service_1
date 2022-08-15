using System.ComponentModel.DataAnnotations;
using AuditService.Common.Models.Interfaces;

namespace AuditService.Common.Models.Domain.LossesLog;

/// <summary>
///     Losses log base model
/// </summary>
public abstract class LossesLogBaseModel : INodeId
{
    protected LossesLogBaseModel()
    {
        Login = string.Empty;
        CurrencyCode = string.Empty;
    }

    /// <summary>
    ///     Node ID of the player who placed the bet
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Login of the player who placed the bet
    /// </summary>
    [Required]
    public string Login { get; set; }

    /// <summary>
    ///     ID of the player who placed the bet
    /// </summary>
    [Required]
    public Guid PlayerId { get; set; }

    /// <summary>
    ///     The currency code of the player's account on which the bet was made
    /// </summary>
    [Required]
    public string CurrencyCode { get; set; }

    /// <summary>
    ///     The amount of the last deposit of the player's account on which the bet was made
    /// </summary>
    [Required]
    public decimal LastDeposit { get; set; }
}