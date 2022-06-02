using Microsoft.Extensions.FileProviders;

namespace AuditService.ELK.FillTestData;

public class EnvironmentPathBuilder
{
    /// <summary>
    ///     Get parent root path of application
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public IFileProvider? GetParentRootPath()
    {
        var directoryInfo = new DirectoryInfo(Environment.CurrentDirectory);
        var configPath = GetParent(directoryInfo)?.FullName;
        return string.IsNullOrEmpty(configPath) ? null : new PhysicalFileProvider(configPath);
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