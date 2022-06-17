using AuditService.Data.Domain.Enums;
using AuditService.Utility.Helpers;
using AuditService.WebApiApp.Services;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Справочник категорий сервисов
/// </summary>
internal class CategoryDictionary
{
    private readonly IConfiguration _configuration;
    private readonly IReferenceService _referenceService;

    public CategoryDictionary(IConfiguration configuration)
    {
        _configuration = configuration;
        var jsonData = new JsonData(configuration);
        _referenceService = new ReferenceService(jsonData, null);
    }

    /// <summary>
    ///     Получить случайную категорию
    /// </summary>
    /// <param name="service">Тип сервиса</param>
    /// <param name="random">Рандомайзер</param>
    public string GetCategory(ServiceId service, Random random)
    {
        var cc = _configuration.GetSection("Categories");
        var category = _referenceService.GetCategoriesAsync().GetAwaiter().GetResult().FirstOrDefault(cat => cat.Key == service);
        
        if (category.Value == null || !category.Value.Any())
            return string.Empty;

        var index = random.Next(category.Value.Length - 1);
        return JsonHelper.SerializeToString(category.Value[index]);
    }
}