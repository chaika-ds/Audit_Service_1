using AuditService.Common.Helpers;

namespace AuditService.Tests.Tests.Data;

/// <summary>
/// Unit Test for JsonHelper class
/// </summary>
public class JsonHelperTest
{
    /// <summary>
    /// Unit Test for SerializeToString method
    /// </summary>
    [Fact]
    public void SerializeToStringTest()
    {
        //Arrange
        var obj = new
        {
            Description = "Anonymous Object For Test"
        };

        //Act 
        var result = obj.SerializeToString();

        //Assert
        IsType<string>(result);
    }
}