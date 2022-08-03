using AuditService.Tests.Factories.Interfaces;

namespace AuditService.Tests.Factories.Models;

/// <summary>
///     Redis Mock Model 
/// </summary>
public class RedisMock : IBaseMock
{
    /// <summary>
    ///     Redis Key
    /// </summary>
    public string? RedisKey { get; set; }
    
    /// <summary>
    ///     Redis Value
    /// </summary>
    public string? RedisValue { get; set; }
    
    /// <summary>
    ///     Redis Expire time in minute
    /// </summary>
    public int ExpireInMinute { get; set; }
}