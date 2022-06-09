using AuditService.Data.Domain.Enums;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Модель генерации данных
/// </summary>
internal class ConfigurationModel
{
    /// <summary>
    ///     Идентификатор сервиса
    /// </summary>
    public ServiceId? ServiceName { get; set; }

    /// <summary>
    ///     Тип действия
    /// </summary>
    public ActionType? ActionName { get; set; }

    /// <summary>
    ///     Категория действий (в зависимости от модулей)
    /// </summary>
    public string? CategoryCode { get; set; }

    /// <summary>
    ///     Тип узла
    /// </summary>
    public NodeType? NodeType { get; set; }

    /// <summary>
    ///     Количество записей для генерации
    /// </summary>
    public int Count { get; set; }
}