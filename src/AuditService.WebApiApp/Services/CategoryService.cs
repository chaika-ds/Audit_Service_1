using AuditService.Common;
using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.Models;
using AuditService.WebApiApp.Models.Responses;
using AuditService.WebApiApp.Services.Interfaces;
using Newtonsoft.Json;

namespace AuditService.WebApiApp.Services;

public class CategoryService : ICategory
{
    private readonly IProjectSettings _projectSettings;
    public CategoryService(IProjectSettings projectSettings)
    {
        _projectSettings = projectSettings;
    }
    
    public async Task<Dictionary<string, object>> GetFilteredCategoryAsync(ServiceName? serviceName)
    {
        var dict = new Dictionary<string, object>();

        using var reader = new StreamReader(_projectSettings.ServiceCategoriesJsonPath);
        var json = await reader.ReadToEndAsync();

        var data = JsonConvert.DeserializeObject<ServiceCategories<Category>>(json);

        if (serviceName == null)
        {
            foreach (var key in data.Select(x => x.Key))
            {
                var value = data.Where(filter => filter.Key == key).Select(x => x.Value);

                dict.Add(key, value);
            }

            return dict;
        }

        var values = data?.Where(x => x.Key == serviceName.ToString()).Select(x => x.Value);

        if (values != null) dict.Add(serviceName.ToString(), values);

        return dict;
    }

    public List<EnumListResponse> GetAllService()
    {
        var values = Enum.GetValues(typeof(ServiceName));
        List<EnumListResponse> enumList = new List<EnumListResponse>();
        
        foreach (var item in values)
        {
            var value = Enum.GetName(typeof(ServiceName), item);
            enumList.Add(new EnumListResponse()
            {
                Name = value,
                Value = (int) item
            });
        }

        return enumList;
    }
}

