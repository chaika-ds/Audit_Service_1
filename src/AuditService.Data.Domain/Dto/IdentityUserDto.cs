namespace AuditService.Data.Domain.Dto;

/// <summary>
///     Аккаунт пользователя
/// </summary>
public class IdentityUserDto
{
    /// <summary>
    ///     Id пользователя
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    ///     IP-адрес пользователя
    /// </summary>
    public string Ip { get; set; }

    /// <summary>
    ///     Логин пользователя
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    ///     Данные о браузере пользователя
    /// </summary>
    public string UserAgent { get; set; }
}