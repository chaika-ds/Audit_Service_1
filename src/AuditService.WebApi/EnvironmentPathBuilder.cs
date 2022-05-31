namespace AuditService.WebApi;

public static class EnvironmentPathBuilder
{
    public static string GetParentRootPath(this IWebHostEnvironment webHost)
    {
        var configsPath = Directory.GetParent(webHost.ContentRootPath)?.Parent?.Parent?.FullName;

        return "";
    }
}