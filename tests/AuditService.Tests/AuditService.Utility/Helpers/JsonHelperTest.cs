using AuditService.Utility.Helpers;

namespace AuditService.Tests.AuditService.Utility.Helpers;

/// <summary>
/// Unit Test for JsonHelper class
/// </summary>
public class JsonHelperTest
{
    /// <summary>
    /// Unit Test for GetJson method
    /// </summary>
    [Fact]
    public void GetJsonTest()
    {
        //Arrange
        var path =  @"JsonModels/MockedData.json";

        //Act 
        var result = JsonHelper.GetJson(path);

        using var streamReader = new StreamReader(path); 
        var expectedResult = streamReader.ReadToEnd();

        //Assert
        Assert.Equal(result,expectedResult);
    }

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
        Assert.IsType<string>(result);
    }
}