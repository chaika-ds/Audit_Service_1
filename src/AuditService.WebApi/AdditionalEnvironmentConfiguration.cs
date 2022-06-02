using Microsoft.Extensions.FileProviders;

namespace AuditService.WebApi;

public class AdditionalEnvironmentConfiguration
{
    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="pathFile"/> to <paramref name="builder"/>.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public void AddJsonFile(WebApplicationBuilder builder, string pathFile)
    {
        if (builder.Environment.ContentRootPath == "/app/")
        {
            builder.Configuration.AddJsonFile(pathFile, true, true);
            return;
        }

        var directoryInfo = new DirectoryInfo(builder.Environment.ContentRootPath);
        var configPath = GetParent(directoryInfo)?.FullName;
        if (string.IsNullOrEmpty(configPath))
        {
            Console.WriteLine($"additional config folder in all parts of path '{directoryInfo.FullName}' - not founded!");
            return;
        }

        var fileProvider = new PhysicalFileProvider(configPath);
        builder.Configuration.AddJsonFile(fileProvider, pathFile, true, true);
    }

    /// <summary>
    ///     Find parent root with name from value
    /// </summary>
    private DirectoryInfo? GetParent(DirectoryInfo? directoryInfo)
    {
        while (true)
        {
            if (directoryInfo == null || !directoryInfo.FullName.Contains("src"))
                return directoryInfo;

            directoryInfo = directoryInfo?.Parent;
        }
    }
}