using AuditService.Common.Enums;
using AuditService.Utility.Helpers;

namespace AuditService.Tests.AuditService.Utility.Helpers;

/// <summary>
/// Unit Test for EnumHelper class
/// </summary>
public class EnumHelperTest
{
    /// <summary>
    /// Unit Test for CheckAndParseChannel method
    /// </summary>
    [Theory]
    [InlineData("uat", LogChannel.uat)]
    [InlineData("development", LogChannel.development)]
    [InlineData("test", LogChannel.test)]
    [InlineData("demo", LogChannel.demo)]
    [InlineData("production", LogChannel.production)]
    [InlineData("randomChannel", LogChannel.wrongChannel)]
    public void CheckAndParseChannelTest(string value, LogChannel expResult)
    {
        //Arrange && Act 
        var result = EnumHelper.CheckAndParseChannel(value);
        
        //Asserts
        Assert.Equal(expResult, result);
    }
}