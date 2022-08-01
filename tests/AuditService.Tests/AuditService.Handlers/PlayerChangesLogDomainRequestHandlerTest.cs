using AuditService.Common.Enums;
using AuditService.Common.Models.Domain.PlayerChangesLog;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Handlers.Handlers.DomainRequestHandlers;
using AuditService.Setup.AppSettings;
using AuditService.Tests.AuditService.Handlers.Fakes;
using AuditService.Tests.Fakes;
using Microsoft.Extensions.DependencyInjection;
using Nest;
using static Xunit.Assert;

namespace AuditService.Tests.AuditService.Handlers;

/// <summary>
/// PlayerChangesLogDomainRequestHandler Test class
/// </summary>
public class PlayerChangesLogDomainRequestHandlerTest : PlayerChangesLogDomainRequestHandler
{

    private readonly IElasticIndexSettings _elasticIndexSettings;
    private static readonly IServiceProvider ServiceProvider = ServiceProviderFake.CreateElkServiceProviderFake();

    public PlayerChangesLogDomainRequestHandlerTest(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        _elasticIndexSettings = ServiceProvider.GetRequiredService<IElasticIndexSettings>();
    }


    /// <summary>
    /// Testing for  GetQueryIndex
    /// </summary>
    [Fact]
    public void PlayerChangesLogDomainRequestHandler_ApplyFilter_Test()
    {
        //Arrange
        //var result = ApplyFilter(queryContainerDescriptor, filter);


        //// var filter = _responses.GetTestPlayerChangesLogFilterDto();
        // QueryContainer expected = new QueryContainer();

        // //Act
        // var result = ApplyFilter(queryContainerDescriptor, filter);

        //Assert
        //Equal(expected, result);
    }

    //[Theory]
    //[MemberData(nameof(GetColumnNameToSortData))]
    //public void GetColumnNameToSort_Test(LogSortDto logSortModel, string expected)
    //{
    //    var result = GetColumnNameToSort(actual);

    //    Equal(expected, result);
    //}


    /// <summary>
    /// Testing for getting Query index from Elastic search
    /// </summary>
    [Fact]
    public void PlayerChangesLogDomainRequestHandler_GetQueryIndex_ReturnQueryIndex()
    {
        //Act
        var queryIndex = GetQueryIndex(_elasticIndexSettings);

        //Assert
        IsPlayerChangesLogReceived(queryIndex);
    }

    /// <summary>
    /// Testing for getting Query index from Elastic search
    /// </summary>
    [Fact]
    public void PlayerChangesLogDomainRequestHandler_GetColumnNameToSort_ReturnNameOfColumnToSort()
    {
        //Arrange
        var logSortModelTest = new LogSortDto()
        {
            SortableType = SortableType.Ascending
        };

        //Act
        var queryIndex = GetColumnNameToSort(logSortModelTest);

        //Assert
        
    }

}