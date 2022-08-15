using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.Common.Models.Dto.Filter;

/// <summary>
///     Filter model for losses log
/// </summary>
public class LossesLogFilterDto : ILogFilter
{
    /// <summary>
    ///     Node ID of the player who placed the bet
    /// </summary>
    public Guid? NodeId { get; set; }

    /// <summary>
    ///     The currency code of the player's account on which the bet was made
    /// </summary>
    public string? CurrencyCode { get; set; }

    /// <summary>
    ///     The amount of the last deposit of the player's account on which the bet was made
    /// </summary>
    public decimal? LastDeposit { get; set; }

    /// <summary>
    ///     Login of the player who placed the bet
    /// </summary>
    public string? Login { get; set; }

    /// <summary>
    ///     ID of the player who placed the bet
    /// </summary>
    public Guid? PlayerId { get; set; }

    /// <summary>
    ///     Filtering by time(Start date)
    /// </summary>
    [Required]
    [ModelBinder(Name = "createdTimeFrom")]
    public DateTime TimestampFrom { get; set; }

    /// <summary>
    ///     Filtering by time(End date)  
    /// </summary>
    [Required]
    [ModelBinder(Name = "createdTimeTo")]
    public DateTime TimestampTo { get; set; }
}