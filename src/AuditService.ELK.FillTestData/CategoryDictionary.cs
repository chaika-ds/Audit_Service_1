using AuditService.Common.Enums;
using AuditService.Providers.Implementations;
using AuditService.Providers.Interfaces;
using AuditService.Utility.Helpers;
using Microsoft.Extensions.Configuration;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Reference of service categories
/// </summary>
internal class CategoryDictionary
{
    private readonly IReferenceProvider _referenceProvider;

    public CategoryDictionary()
    {
        _referenceProvider = new ReferenceProvider();
    }

    /// <summary>
    ///     Get random category of service
    /// </summary>
    /// <param name="service">Serive type</param>
    /// <param name="random">Instance of random function</param>
    public string GetCategory(ServiceStructure service, Random random)
    {
        var category = _referenceProvider.GetCategoriesAsync().GetAwaiter().GetResult().FirstOrDefault(cat => cat.Key == service);
        if (!category.Value.Any())
            return string.Empty;

        var index = random.Next(category.Value.Length - 1);
        return category.Value[index].SerializeToString();
    }
}