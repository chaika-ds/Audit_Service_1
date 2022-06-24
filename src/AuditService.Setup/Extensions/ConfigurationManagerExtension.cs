using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace AuditService.Setup.Extensions;

/// <summary>
///     Extension of configuration manager
/// </summary>
public static class ConfigurationManagerExtension
{
    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="configFile" /> to <paramref name="configuration" />.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public static void AddJsonFile(this ConfigurationManager configuration, string configFile, IHostEnvironment environment)
    {
        var configFilePath = GetJsonFile(configFile, environment);
        var configs = File.ReadAllText(configFilePath);

        configuration.AddJsonStream(new MemoryStream(Encoding.Default.GetBytes(configs)));
    }

    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="configFile" /> to <paramref name="configuration" /> with environments from <paramref name="envFile" />.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public static void AddJsonFile(this ConfigurationManager configuration, string configFile, string envFile, IHostEnvironment environment)
    {
        var configFilePath = GetJsonFile(configFile, environment);
        var envFilePath = GetJsonFile(envFile, environment);
        
        var configs = File.ReadAllText(configFilePath);
        var environments = File.ReadAllText(envFilePath);

        Console.WriteLine(configs);
        Console.WriteLine(environments);

        var envs = JsonConvert.DeserializeObject<IDictionary<string, string>>(environments);
        if (envs != null) 
            configs = envs.Aggregate(configs, (current, env) => current.Replace($"${env.Key}", env.Value));

        configuration.AddJsonStream(new MemoryStream(Encoding.Default.GetBytes(configs)));
    }

    /// <summary>
    ///     Get JSON file
    /// </summary>
    /// <param name="pathFile">File path</param>
    /// <param name="environment">Host environment</param>
    /// <returns>Path to JSON file</returns>
    private static string GetJsonFile(string pathFile, IHostEnvironment environment)
    {
        if (environment.ContentRootPath == "/app/")
            return Path.Combine(environment.ContentRootPath, pathFile);

        var directoryInfo = new DirectoryInfo(environment.ContentRootPath);
        var configPath = GetParent(directoryInfo)?.FullName;
        if (string.IsNullOrEmpty(configPath))
        {
            Console.WriteLine($"additional config folder in all parts of path '{directoryInfo.FullName}' - not founded!");
            return pathFile;
        }

        return Path.Combine(configPath, pathFile);
    }

    /// <summary>
    ///     Find parent root with name from value
    /// </summary>
    private static DirectoryInfo? GetParent(DirectoryInfo? directoryInfo)
    {
        while (true)
        {
            if (directoryInfo?.FullName.Contains("src") != true)
                return directoryInfo;

            directoryInfo = directoryInfo?.Parent;
        }
    }
}