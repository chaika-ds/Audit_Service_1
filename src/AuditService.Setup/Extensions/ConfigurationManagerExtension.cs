﻿using System.Collections;
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
    ///     If the environment file does not exist, then environment variables will be applied.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    public static void AddJsonFile(this ConfigurationManager configuration, string configFile, string envFile, IHostEnvironment environment)
    {
        if (File.Exists(GetJsonFile(envFile, environment)))
            configuration.AddJsonFileWithEnvironmantFile(configFile, envFile, environment);
        else
            configuration.AddJsonFileWithEnvironmentVariables(configFile, environment);
    }

    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="configFile" /> to <paramref name="configuration" /> with environments from <paramref name="envFile" />.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    private static void AddJsonFileWithEnvironmantFile(this IConfigurationBuilder configuration, string configFile, string envFile, IHostEnvironment environment)
    {
        var configFilePath = GetJsonFile(configFile, environment);
        var environmentFilePath = GetJsonFile(envFile, environment);
        
        var configs = File.ReadAllText(configFilePath);
        var envData = File.ReadAllText(environmentFilePath);
        var environments = JsonConvert.DeserializeObject<IDictionary<string, string>>(envData);

        var config = environments?.Aggregate(configs, (current, env) => current.Replace($"${env.Key}", env.Value));
        if (config != null)
            configuration.AddJsonStream(new MemoryStream(Encoding.Default.GetBytes(config)));
    }

    /// <summary>
    ///     Adds the JSON configuration provider at <paramref name="configFile" /> to <paramref name="configuration" /> with environments from EnvironmentsVariables.
    /// </summary>
    /// <remarks>
    ///     Supported docker container directory
    /// </remarks>
    private static void AddJsonFileWithEnvironmentVariables(this IConfigurationBuilder configuration, string configFile, IHostEnvironment environment)
    {
        var configFilePath = GetJsonFile(configFile, environment);
        var configs = File.ReadAllText(configFilePath);
        
        configs = Environment.GetEnvironmentVariables().Cast<DictionaryEntry>().Aggregate(configs, (current, env) => current.Replace($"${env.Key}", env.Value?.ToString()));
        
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
            Console.WriteLine($@"additional config folder in all parts of path '{directoryInfo.FullName}' - not founded!");
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