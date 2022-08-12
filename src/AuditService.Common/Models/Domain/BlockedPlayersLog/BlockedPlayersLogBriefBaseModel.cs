using System.ComponentModel.DataAnnotations;
using AuditService.Common.Models.Interfaces;

namespace AuditService.Common.Models.Domain.BlockedPlayersLog;

/// <summary>
///     Blocked player log brief base model
/// </summary>
public abstract class BlockedPlayersLogBriefBaseModel : INodeId
{
    protected BlockedPlayersLogBriefBaseModel()
    {
        PlayerLogin = string.Empty;
        Browser = string.Empty;
        Language = string.Empty;
        BrowserVersion = string.Empty;
    }

    /// <summary>
    ///     Player node Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Login of the blocked player
    ///     (if not - then mail, if there is no mail - phone)
    /// </summary>
    [Required]
    public string PlayerLogin { get; set; }

    /// <summary>
    ///     Blocked player Id
    /// </summary>
    [Required]
    public Guid PlayerId { get; set; }

    /// <summary>
    ///     Date and time the lock was set
    /// </summary>
    [Required]
    public DateTime BlockingDate { get; set; }

    /// <summary>
    ///     Date and time of the previous block
    /// </summary>
    public DateTime? PreviousBlockingDate { get; set; }

    /// <summary>
    ///     Counter of the number of player bans
    ///     (1 - if the first, 2 - if the second, etc.)
    /// </summary>
    [Required]
    public int BlocksCounter { get; set; }

    /// <summary>
    ///     Browser that the player logged in from
    /// </summary>
    [Required]
    public string Browser { get; set; }

    /// <summary>
    ///     Browser version
    /// </summary>
    [Required]
    public string BrowserVersion { get; set; }

    /// <summary>
    ///     Player interface language code
    /// </summary>
    [Required]
    public string Language { get; set; }
}