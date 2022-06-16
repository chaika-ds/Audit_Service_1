using AuditService.Common.Enums;
using AuditService.Providers.Implementations;
using AuditService.Providers.Interfaces;
using AuditService.Utility.Helpers;
using Microsoft.Extensions.Configuration;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Справочник категорий сервисов
/// </summary>
internal class CategoryDictionary
{
    private readonly IReferenceProvider _referenceProvider;

    public CategoryDictionary(IConfiguration configuration)
    {
        _referenceProvider = new ReferenceProvider(new JsonData(configuration));
    }

    /// <summary>
    ///     Получить случайную категорию
    /// </summary>
    /// <param name="service">Тип сервиса</param>
    /// <param name="random">Рандомайзер</param>
    public string GetCategory(ServiceId service, Random random)
    {
        var category = _referenceProvider.GetCategoriesAsync().GetAwaiter().GetResult().FirstOrDefault(cat => cat.Key == service);
        if (!category.Value.Any())
            return string.Empty;

        var index = random.Next(category.Value.Length - 1);
        return category.Value[index].SerializeToString();
    }
}