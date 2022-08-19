using AuditService.Common.Models.Dto;
using AuditService.Common.Models.Dto.Filter;
using AuditService.Common.Models.Dto.Sort;
using AuditService.Tests.Fakes.ServiceData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Text;

namespace AuditService.Tests.Helpers.Journals;

/// <summary>
///     Journals test helper
/// </summary>
/// <typeparam name="TFilter">Filter model type</typeparam>
/// <typeparam name="TSort">Sort model type</typeparam>
/// <typeparam name="TDomainModel">Domain model type</typeparam>
/// <typeparam name="TResponse">Response model type</typeparam>
internal static class LogsTestHelper<TFilter, TSort, TResponse, TDomainModel>
    where TFilter : class, ILogFilter, new()
    where TSort : class, ISort, new()
    where TResponse : class
    where TDomainModel : class
{
    /// <summary>
    ///     Check if the result returning
    /// </summary>
    /// <param name="elkIndex">Elk index</param>
    /// <param name="testDataJsonModel">Test data in json formate</param>
    internal static async Task CheckReturnResult(string elkIndex,
            byte[] testDataJsonModel)
    {
        var response = await GetLogHandlerResponse(elkIndex, testDataJsonModel);

        NotEmpty(response.List);
    }

    /// <summary>
    ///     Get log handler response
    /// </summary>
    /// <param name="elkIndex"></param>
    /// <param name="testDataJsonModel"></param>
    /// <returns>Page response</returns>
    internal static async Task<PageResponseDto<TResponse>> GetLogHandlerResponse(string elkIndex,
            byte[] testDataJsonModel)
    {
        var serviceProvider = ServiceProviderFake
            .GetServiceProviderForLogHandlers<TDomainModel>(testDataJsonModel, elkIndex);

        var mediatorService = serviceProvider.GetRequiredService<IMediator>();

        var filter = new LogFilterRequestDto<TFilter, TSort, TResponse>()
        {
            Filter = new()
            {
                TimestampFrom = DateTime.Now,
                TimestampTo = DateTime.Now
            }
        };

        var response = await mediatorService.Send(filter, new TaskCanceledException().CancellationToken);

        return response;
    }

    /// <summary>
    ///     Get expected domain model
    /// </summary>
    /// <param name="elkIndex">Elk index</param>
    /// <param name="testDataJsonModel">Test data json model</param>
    /// <returns>List of domain models</returns>
    internal static List<TDomainModel> GetExpectedDomainModels(string elkIndex,
            byte[] testDataJsonModel)
    {
        var expected = JsonConvert.DeserializeObject<List<TDomainModel>>(Encoding.Default.GetString(testDataJsonModel));

        return expected;
    }
}
