using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.Models.Responses;

namespace AuditService.WebApiApp.Services.Interfaces;

public interface ICategory
{
    Task<Dictionary<string, object>> GetFilteredCategoryAsync(ServiceName serviceName);
    List<EnumListResponse> GetAllService();
}