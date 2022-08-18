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
    public class CategoriesActionTest
    {
        private readonly IMediator _mediatorService;

        private const string category = "PAYMENTS_PPS_KEYS";

        private const string wrongCategory = "TEST";

        public CategoriesActionTest()
        {
            var serviceProvider = ServiceProviderFake.GetServiceProviderForLogHandlers();
            _mediatorService = serviceProvider.GetRequiredService<IMediator>();
        }

        [Fact]
        public async Task GetActionsByCategoryCode_ActionsInResource_ResultedActionsCorrespondedToResource()
        {
            //Arrange
            var request = new GetActionsRequest(category);

            var expected = GetCategories()
                .SelectMany(x => x.Value)
                .Select(x => x.Action)
                .SelectMany(x => x);

            //Act 
            var actual = await _mediatorService.Send(request, new TaskCanceledException().CancellationToken);

            //Assert
            foreach(var action in actual)
            {
                NotNull(expected
                    ?.FirstOrDefault(x => x.Name == action.Name
                    && x.Description == action.Description));
            }
        }

        [Fact]
        public async Task GetCategories_CategoriesInResource_EmptyResponse()
        {
            //Arrange
            var request = new GetActionsRequest(wrongCategory);

            var expected = GetCategories()
                .SelectMany(x => x.Value)
                .Select(x => x.Action)
                .SelectMany(x => x);

            //Act 
            var actual = await _mediatorService.Send(request, new TaskCanceledException().CancellationToken);

            //Assert
            Null(actual);
        }


        private IDictionary<ModuleName, CategoryDomainModel[]> GetCategories()
            => JsonConvert.DeserializeObject<IDictionary<ModuleName, CategoryDomainModel[]>>(System.Text.Encoding.Default.GetString(JsonResource.ServiceCategories));
    }
}
