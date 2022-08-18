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
    public class PlayerCangesLogEventsTest
    {
        private readonly IMediator _mediatorService;

        private const ModuleName module = ModuleName.LAB;

        public PlayerCangesLogEventsTest()
        {
            var serviceProvider = ServiceProviderFake.GetServiceProviderForLogHandlers();
            _mediatorService = serviceProvider.GetRequiredService<IMediator>();
        }
        /// <summary>
        ///     Check if resulted events corresponded to resource
        /// </summary>
        [Fact]
        public async Task GetEvents_EventsInResource_ResultedEventsCorrespondedToResource()
        {
            //Arrange
            var request = new GetEventsRequest(module);

            var expected = GetEvents();

            var expectedUnit = expected.FirstOrDefault(x => x.Key == module);

            //Act 
            var actual = await _mediatorService.Send(request, new TaskCanceledException().CancellationToken);

            var actualUnit = actual.FirstOrDefault(x => x.Key == expectedUnit.Key);

            //Assert
            Equal(expectedUnit.Key, actualUnit.Key);
            Equal(expectedUnit.Value.FirstOrDefault()?.Description, actualUnit.Value.FirstOrDefault()?.Description);
            Equal(expectedUnit.Value.FirstOrDefault()?.OldOrNewValue, actualUnit.Value.FirstOrDefault()?.OldOrNewValue);
            Equal(expectedUnit.Value.FirstOrDefault()?.Event, actualUnit.Value.FirstOrDefault()?.Event);
            Equal(expectedUnit.Value.FirstOrDefault()?.Name, actualUnit.Value.FirstOrDefault()?.Name);
        }

        private IDictionary<ModuleName, EventDomainModel[]> GetEvents()
          => JsonConvert.DeserializeObject<IDictionary<ModuleName, EventDomainModel[]>>(System.Text.Encoding.Default.GetString(JsonResource.ServiceEvents));
    }
}
