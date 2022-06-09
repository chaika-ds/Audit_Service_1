using AuditService.Common.Helpers;
using AuditService.Common.Logger;
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

    /// <summary>
    /// Adds customer logger provider at <paramref name="environmentName"/> to <paramref name="builder"/>.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="environmentName"></param>
    public static void AddCustomerLogger(WebApplicationBuilder builder, string environmentName)
    {
        builder.Logging.ClearProviders();
        builder.Logging.SetMinimumLevel(LogLevel.Trace);
        builder.Logging.AddAuditServiceLogger(options => {
            builder.Configuration.Bind(options);
            options.Channel = EnumHelper.CheckAndParseChannel(environmentName.ToLower());
        });
    }
}