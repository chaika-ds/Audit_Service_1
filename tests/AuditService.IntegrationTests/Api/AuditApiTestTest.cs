using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AuditService.Data.Domain.Dto.Filter;
using AuditService.Data.Domain.Dto.Pagination;
using AuditService.IntegrationTests.Api.Helpers;
using Xunit;


namespace AuditService.IntegrationTests.Api;

public class AuditApiTestTest : BaseApiTest
{
    [Fact]
    public async Task Get_Audit_GetLogAsync_Return_FilteredLogsAsync()
    {
        var inputModel = new AuditLogFilterRequestDto()
        {
            Pagination =  new PaginationRequestDto()
            {
                PageSize = 1,
                PageNumber = 0
            }
        };
        
        var url = $"{Localhost}/audit/log";
        
        await Auth.AuthenticationService();
        
        var response = await HttpHelper.SendAsync(url, inputModel, Auth.NodeId.ToString(), Auth.Token, HttpMethod.Get );

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}
