namespace AuditService.WebApiApp;

public interface IProjectSettings
{
    string ServiceCategoriesJsonPath { get; set; }
    string SsoBaseUrl { get; set; }
}