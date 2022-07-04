using AuditService.Common.Enums;
using AuditService.Common.Models.Dto;
using AuditService.Utility.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using MediatR;

namespace AuditService.ELK.FillTestData;

/// <summary>
///     Reference of service categories
/// </summary>
public class CategoryDictionary
{
    private readonly IMediator _mediator;

    public CategoryDictionary(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    ///     Get random category of service
    /// </summary>
    /// <param name="service">Serive type</param>
    /// <param name="random">Instance of random function</param>
    /// <param name="cancellationToken">Cancellation token</param>
    public async Task<string> GetCategoryAsync(ServiceStructure service, Random random)
    {
        var category =
            (await _mediator.Send(new GetCategoriesRequest())).FirstOrDefault(cat => cat.Key == service);

        if (!category.Value.Any())
            return string.Empty;

        var index = random.Next(category.Value.Length - 1);
        return category.Value[index].SerializeToString();
    }
}