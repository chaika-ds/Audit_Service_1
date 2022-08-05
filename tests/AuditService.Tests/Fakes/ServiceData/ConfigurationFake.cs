using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;

namespace AuditService.Tests.Fakes.ServiceData;

/// <summary>
/// Fake Configuration class for DI unit test
/// </summary>
public class ConfigurationFake : IConfiguration
{
    /// <summary>
    /// GetChildren fake
    /// </summary>
    /// <returns>new IConfigurationSection[]{}</returns>
    public IEnumerable<IConfigurationSection> GetChildren()
    {
        return new IConfigurationSection[] { };
    }

    /// <summary>
    /// GetReloadToken fake
    /// </summary>
    /// <returns>new CancellationChangeToken(new CancellationToken())</returns>
    public IChangeToken GetReloadToken()
    {
        return new CancellationChangeToken(new CancellationToken());
    }

    /// <summary>
    /// GetSection fake
    /// </summary>
    /// <param name="key">key</param>
    /// <returns>new ConfigurationSection(new ConfigurationManager(), "fakePath")</returns>
    public IConfigurationSection GetSection(string key)
    {
        return new ConfigurationSection(new ConfigurationManager(), "fakePath");
    }

    /// <summary>
    /// key
    /// </summary>
    /// <param name="key">key</param>
    /// <returns>string key</returns>
    public string this[string key]
    {
        get => "testKey";
        set { }
    }
}