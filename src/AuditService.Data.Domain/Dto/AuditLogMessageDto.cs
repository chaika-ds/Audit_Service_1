using System.ComponentModel.DataAnnotations;
using AuditService.Data.Domain.Enums;

namespace AuditService.Data.Domain.Dto;

/// <summary>
///     Audit log tramsaction.
/// </summary>
public class AuditLogTransactionDto
{
    public AuditLogTransactionDto()
    {
        User = new IdentityUserDto();
    }

    /// <summary>
    ///     Идентификатор сервиса.
    /// </summary>
    [Required]
    public ServiceName ServiceName { get; set; }
namespace AuditService.Data.Domain.Dto
{
    /// <summary>
    /// Audit log tramsaction.
    /// </summary>
    public class AuditLogMessageDto
    {
        /// <summary>
        /// Идентификатор сервиса.
        /// </summary>
        [Required()]
        public ServiceName ServiceName { get; set; }

    /// <summary>
    ///     Идентификатор узла, на котором произошло изменение.
    /// </summary>
    [Required]
    public Guid NodeId { get; set; }

    /// <summary>
    ///     Тип узла.
    /// </summary>
    [Required]
    public NodeTypes NodeType { get; set; }

    /// <summary>
    ///     сумма депозита.
    /// </summary>
    [Required]
    public ActionNameType ActionName { get; set; }

    /// <summary>
    ///     Категория действий (в зависимости от модулей).
    /// </summary>
    [Required]
    public string CategoryCode { get; set; }

    /// <summary>
    ///     Текстовое представление запроса.
    /// </summary>
    public string RequestUrl { get; set; }

    /// <summary>
    ///     JSON представление тела запроса.
    /// </summary>
    public string RequestBody { get; set; }

    /// <summary>
    ///     Дата и время события(стандарт ISO 8601 UTC).
    /// </summary>
    [Required]
    public DateTime Timestamp { get; set; }

    /// <summary>
    ///     Название класса (или таблицы в базе) логируемой сущности.
    /// </summary>
    public string EntityName { get; set; }

        /// <summary>
        /// Идентификатор логируемой сущности(возможные типы значений UUID/Long).
        /// </summary>
        public Guid EntityId { get; set; }

    /// <summary>
    ///     JSON представление предыдущего значения сущности.
    /// </summary>
    public string OldValue { get; set; }

    /// <summary>
    ///     JSON представление нового значения сущности.
    /// </summary>
    public string NewValue { get; set; }

    /// <summary>
    ///     ID проекта, с которого логируется изменение в аудит.
    /// </summary>
    public Guid ProjectId { get; set; }

    /// <summary>
    ///     Пользователь
    /// </summary>
    public IdentityUserDto User { get; set; }
}