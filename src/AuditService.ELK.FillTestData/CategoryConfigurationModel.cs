using AuditService.Data.Domain.Enums;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Модель конфигурации категории
/// </summary>
internal class CategoryConfigurationModel
{
    public CategoryConfigurationModel()
    {
        Items = new List<string>();
    }

    /// <summary>
    ///     Тип сервиса
    /// </summary>
    public ServiceIdentity ServiceName { get; set; }

    /// <summary>
    ///     Список категорий
    /// </summary>
    public IList<string> Items { get; set; }
}