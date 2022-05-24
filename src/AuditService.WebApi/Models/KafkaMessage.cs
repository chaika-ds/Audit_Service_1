namespace AuditService.WebApi.Models;

public class KafkaMessage
{
    /// <summary>
    /// Идентификатор сервиса.
    /// </summary>
    public string ServiceName { get; set; }
    /// <summary>
    /// Идентификатор узла, на котором произошло изменение
    /// </summary>
    public Guid NodeId { get; set; }
    /// <summary>
    /// Тип узла
    /// </summary>
    public string NodeType { get; set; }
    /// <summary>
    /// Тип действия (CREATE, EDIT, DELETE, SOFT_DELETE, EXPORT, SYNCHRONIZATION, LOGIN, LOGOUT)
    /// </summary>
    public string ActionName { get; set; }
    /// <summary>
    /// Категория действий (в зависимости от модулей)
    /// </summary>
    public string CategoryCode { get; set; }
    /// <summary>
    /// Текстовое представление запроса (напр. "PUT: contracts/contractId?param=value")
    /// </summary>
    public string RequestUrl { get; set; }
    /// <summary>
    /// JSON представление тела запроса
    /// </summary>
    public string RequestBody { get; set; }
    /// <summary>
    /// Дата и время события(стандарт ISO 8601 UTC). Формат: YYYY-MM-DD HH:MM:SS
    /// </summary>
    public DateTime Timestamp { get; set; }
    /// <summary>
    /// Название класса (или таблицы в базе) логируемой сущности
    /// </summary>
    public string EntityName { get; set; }
    /// <summary>
    /// Идентификатор логируемой сущности(возможные типы значений UUID/Long)
    /// </summary>
    public string EntityId { get; set; }
    /// <summary>
    /// JSON представление предыдущего значения сущности
    /// </summary>
    public string OldValue { get; set; }
    /// <summary>
    /// JSON представление нового значения сущности
    /// </summary>
    public string NewValue { get; set; }
    /// <summary>
    /// ID проекта, с которого логируется изменение в аудит
    /// </summary>
    public Guid ProjectId { get; set; }
    /// <summary>
    /// Id пользователя админпанели или игрока, который сделал изменения
    /// </summary>
    public Guid UserId { get; set; }
    /// <summary>
    /// IP-адрес пользователя
    /// </summary>
    public string UserIp { get; set; }
    /// <summary>
    /// Login пользователя
    /// </summary>
    public string UserLogin { get; set; }
    /// <summary>
    /// Данные о браузере пользователя
    /// </summary>
    public string UserAgent { get; set; }
}