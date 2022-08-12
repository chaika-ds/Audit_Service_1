using System.ComponentModel.DataAnnotations;

namespace AuditService.Common.Models.Domain.SsoPlayerChangesLog;

/// <summary>
///     SSO player changes log
/// </summary>
public class SsoPlayerChangesLogDomainModel
{
    public SsoPlayerChangesLogDomainModel()
    {
        LastVisitIp = string.Empty;
        EventType = string.Empty;
        PlayerAuthorization = new AuthorizationDataDomainModel();
    }

    /// <summary>
    ///     NodeId Id
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Player Id
    /// </summary>
    [Required]
    public Guid PlayerId { get; set; }

    /// <summary>
    ///     Player login
    /// </summary>
    public string? Login { get; set; }

    /// <summary>
    ///     Player nickname
    /// </summary>
    public string? Nickname { get; set; }

    /// <summary>
    ///     Player first name
    /// </summary>
    public string? FirstName { get; set; }

    /// <summary>
    ///     Player middle name
    /// </summary>
    public string? MiddleName { get; set; }

    /// <summary>
    ///     Player last name
    /// </summary>
    public string? LastName { get; set; }

    /// <summary>
    ///     Gender of the player
    /// </summary>
    public int? Gender { get; set; }

    /// <summary>
    ///     Player date of birth
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    ///     Lang code
    /// </summary>
    public string? LangCode { get; set; }

    /// <summary>
    ///     IP from which the player registered
    /// </summary>
    public string? RegistrationIp { get; set; }

    /// <summary>
    ///     Date and time of registration
    /// </summary>
    public DateTime? RegisteredAt { get; set; }

    /// <summary>
    ///     Player email
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    ///     Player phone
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    ///     List of social media links
    /// </summary>
    public List<string>? SocialNetworks { get; set; }

    /// <summary>
    ///     Player country code
    /// </summary>
    public string? CountryCode { get; set; }

    /// <summary>
    ///     Player city
    /// </summary>
    public string? City { get; set; }

    /// <summary>
    ///     Player address
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    ///     Player postcode
    /// </summary>
    public string? Postcode { get; set; }

    /// <summary>
    ///     Player timezone
    /// </summary>
    public string? Timezone { get; set; }

    /// <summary>
    ///     Whether the player has been verified
    /// </summary>
    [Required]
    public int IsVerified { get; set; }

    /// <summary>
    ///     Player status
    /// </summary>
    [Required]
    public int Status { get; set; }

    /// <summary>
    ///     Email confirmation status
    /// </summary>
    [Required]
    public int ConfirmationEmailStatus { get; set; }

    /// <summary>
    ///     Phone confirmation status
    /// </summary>
    [Required]
    public int ConfirmationPhoneStatus { get; set; }

    /// <summary>
    ///     IP address from which the user last logged in
    /// </summary>
    [Required]
    public string LastVisitIp { get; set; }

    /// <summary>
    ///     Date and time of last visit
    /// </summary>
    public DateTime? LastVisitAt { get; set; }

    /// <summary>
    ///     Event type
    /// </summary>
    [Required]
    public string EventType { get; set; }

    /// <summary>
    ///     Event date time
    /// </summary>
    [Required]
    public DateTime EventDateTime { get; set; }

    /// <summary>
    ///     Player cookies
    /// </summary>
    public object? Cookie { get; set; }

    /// <summary>
    ///     Numeric player Id(old field)
    /// </summary>
    public int? OldCasinoPlayerId { get; set; }

    /// <summary>
    ///     Old player Id
    /// </summary>
    public int? OldPlayerId { get; set; }

    /// <summary>
    ///     String Id of the player (old field)
    /// </summary>
    public string? OldExternalPlayerId { get; set; }

    /// <summary>
    ///     Bonus ID that was selected during registration
    /// </summary>
    public Guid? BonusID { get; set; }

    /// <summary>
    ///     Identifier of the currency selected during registration
    /// </summary>
    public Guid? CurrencyID { get; set; }

    /// <summary>
    ///     Player authorization data
    /// </summary>
    [Required]
    public AuthorizationDataDomainModel PlayerAuthorization { get; set; }
}