using Microsoft.Extensions.FileProviders;

namespace AuditService.WebApi.Extensions;

public static class ConfigurationManagerExtension
{
    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="pathFile"/> to <paramref name="configuration"/>.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public static void AddJsonFile(this ConfigurationManager configuration, string pathFile, IWebHostEnvironment environment)
    {
        if (environment.ContentRootPath == "/app/")
        {
            configuration.AddJsonFile(pathFile, true, true);
            return;
        }

        var directoryInfo = new DirectoryInfo(environment.ContentRootPath);
        var configPath = GetParent(directoryInfo)?.FullName;
        if (string.IsNullOrEmpty(configPath))
        {
            Console.WriteLine($"additional config folder in all parts of path '{directoryInfo.FullName}' - not founded!");
            return;
        }

        var fileProvider = new PhysicalFileProvider(configPath);
        configuration.AddJsonFile(fileProvider, pathFile, true, true);
    }

    /// <summary>
    ///     Find parent root with name from value
    /// </summary>
    private static DirectoryInfo? GetParent(DirectoryInfo? directoryInfo)
    {
        while (true)
        {
            if (directoryInfo == null || !directoryInfo.FullName.Contains("src"))
                return directoryInfo;

            directoryInfo = directoryInfo?.Parent;
        }
    }
}