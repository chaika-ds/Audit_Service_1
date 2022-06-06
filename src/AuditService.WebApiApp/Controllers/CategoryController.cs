using AuditService.Data.Domain.Enums;
using AuditService.WebApiApp.Models.Responses;
using AuditService.WebApiApp.Services;
using AuditService.WebApiApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AuditService.WebApiApp.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class CategoryController : ControllerBase
{
    private readonly ICategory _category;
    
    public CategoryController(ICategory category)
    {
        _category = category;
    }
    
    [HttpGet]
    public async Task<Dictionary<string, object>> GetCategoryAsync(ServiceName serviceName)
    {
        return await _category.GetFilteredCategoryAsync(serviceName);
    }
    
    [HttpGet]
    public List<EnumListResponse> ServiceList()
    {
        return  _category.GetAllService();
    }
}