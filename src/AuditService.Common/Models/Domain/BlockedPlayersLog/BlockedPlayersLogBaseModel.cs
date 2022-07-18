using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.BlockedPlayersLog;

/// <summary>
///     Blocked player log base model
/// </summary>
public abstract class BlockedPlayersLogBaseModel
{
    protected BlockedPlayersLogBaseModel()
    {
        PlayerLogin = string.Empty;
        Browser = string.Empty;
        Language = string.Empty;
        BrowserVersion = string.Empty;
    }

    /// <summary>
    ///     Player hall Id
    /// </summary>
    [Required]
    public Guid HallId { get; set; }

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

    /// <summary>
    ///     Date and time of the blocked (ISO 8601 UTC standard)
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }
}