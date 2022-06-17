namespace AuditService.Common.Models.Domain;

/// <summary>
///     User info
/// </summary>
public class IdentityUserDomainModel
{
    public IdentityUserDomainModel()
    {
        Ip = string.Empty;
        Login = string.Empty;
        UserAgent = string.Empty;
    }

    /// <summary>
    ///     User ID
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     IP-adress
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    ///     Login
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    ///     Data about internet browser
    /// </summary>
    public string UserAgent { get; set; }
}