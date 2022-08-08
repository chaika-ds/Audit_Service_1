using AuditService.Common.Enums;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Resources;
using AuditService.Tests.Fakes.ServiceData;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AuditService.Tests.Tests.Handlers.ReferenceRequestHandlerTests
{
    public class CategoriesTest
    {
        private readonly IMediator _mediatorService;

        private const ModuleName moduleName = ModuleName.PAY;

        public CategoriesTest()
        {
            var serviceProvider = ServiceProviderFake.GetServiceProviderForReferenceRequestHandler();
            _mediatorService = serviceProvider.GetRequiredService<IMediator>();
        }

        /// <summary>
        ///     Check if resources are successfully deserialized
        /// </summary>
        [Fact]
        public async Task GetCategories_CategoriesInResource_ResourcesAreSuccessfullyDeserialized()
        {
            //Arrange
            _ = GetCategories();

            //Assert
            True(true);
        }

        /// <summary>
        ///     Check if resulted categories corresponded to resource
        /// </summary>
        [Fact]
        public async Task GetCategories_CategoriesInResource_ResultedCategoriesCorrespondedToResource()
        {
            //Arrange
            var request = new GetCategoriesRequest();

            var expected = GetCategories();

            var expectedUnit = expected.FirstOrDefault();

            //Act 
            var actual = await _mediatorService.Send(request, new TaskCanceledException().CancellationToken);

            var actualUnit = actual.FirstOrDefault(x => x.Key == expectedUnit.Key);

            //Assert
            Equal(expected.Count(), actual.Count());
            Equal(expectedUnit.Key, actualUnit.Key);
            Equal(expectedUnit.Value.FirstOrDefault()?.CategoryCode, actualUnit.Value.FirstOrDefault()?.CategoryCode);
            Equal(expectedUnit.Value.FirstOrDefault()?.CategoryName, actualUnit.Value.FirstOrDefault()?.CategoryName);
            Equal(expectedUnit.Value.FirstOrDefault()?.Action?.FirstOrDefault()?.Name, actualUnit.Value.FirstOrDefault()?.Action?.FirstOrDefault()?.Name);
            Equal(expectedUnit.Value.FirstOrDefault()?.Action?.FirstOrDefault()?.Description, actualUnit.Value.FirstOrDefault()?.Action?.FirstOrDefault()?.Description);
        }

        /// <summary>
        ///     Check if successfully search by module name
        /// </summary>
        [Fact]
        public async Task GetCategories_CategoriesInResource_SuccessfullySearchByModuleName()
        {
            //Arrange
            var request = new GetCategoriesRequest(moduleName);

            //Act 
            var actual = await _mediatorService.Send(request, new TaskCanceledException().CancellationToken);


            //Asser 
            foreach(var category in actual)
            {
                if(category.Key != moduleName)
                {
                    True(false, $"The returned categories do not match the search parameters: {moduleName}");
                }
            }

            True(true);
        }


        private IDictionary<ModuleName, CategoryDomainModel[]> GetCategories()
            => JsonConvert.DeserializeObject<IDictionary<ModuleName, CategoryDomainModel[]>>(System.Text.Encoding.Default.GetString(JsonResource.ServiceCategories));
    }
}
