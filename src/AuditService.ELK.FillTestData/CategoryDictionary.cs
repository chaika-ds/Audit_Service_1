using AuditService.Data.Domain.Enums;
using Microsoft.Extensions.Configuration;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Справочник категорий сервисов
/// </summary>
internal class CategoryDictionary
{
    private readonly IConfiguration _configuration;

    public CategoryDictionary(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// <summary>
    ///     Получить случайную категорию
    /// </summary>
    /// <param name="service">Тип сервиса</param>
    /// <param name="random">Рандомайзер</param>
    public string GetCategory(ServiceId service, Random random)
    {
        var category = _configuration.GetSection("Categories").Get<CategoryConfigurationModel[]>().FirstOrDefault(w => w.ServiceId == service);
        if (category?.Items == null || !category.Items.Any())
            return string.Empty;

        var index = random.Next(category.Items.Count - 1);
        return category.Items[index];
    }
}