namespace AuditService.Tests.Factories.Models;

public class RedisMock : BaseMock
{
    public string? RedisKey { get; set; }
    public string? RedisValue { get; set; }
    public int ExpireInMinute { get; set; }
}