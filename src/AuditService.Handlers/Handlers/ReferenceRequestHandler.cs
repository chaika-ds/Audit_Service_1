using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Resources;
using AuditService.Handlers.PipelineBehaviors.Attributes;
using MediatR;
using Newtonsoft.Json;

namespace AuditService.Handlers.Handlers
{
    /// <summary>
    /// Handler for a request to receive reference resources (services\categories)
    /// </summary>
    [UsePipelineBehaviors(UseCache = true, UseLogging = true)]
    public class ReferenceRequestHandler : IRequestHandler<GetServicesRequest, IEnumerable<EnumResponseDto>>,
        IRequestHandler<GetCategoriesRequest, IDictionary<ModuleName, CategoryDomainModel[]>>,
        IRequestHandler<GetActionsRequest, IEnumerable<ActionDomainModel>?>,
        IRequestHandler<GetEventsRequest, IDictionary<ModuleName, EventDomainModel[]>>
    {
        /// <summary>
        /// Request handler for getting available services.
        /// </summary>
        /// <param name="request">Request for available services</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Available services</returns>
        public Task<IEnumerable<EnumResponseDto>> Handle(GetServicesRequest request, CancellationToken cancellationToken)
        {
            var result = Enum.GetValues<ModuleName>().Select(value => new EnumResponseDto(value.ToString(), value.Description()));
            return Task.FromResult(result);
        }

        /// <summary>
        /// Request handler for getting available categories by serviceId
        /// </summary>
        /// <param name="request">Request for available categories by serviceId</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Available categories</returns>
        public async Task<IDictionary<ModuleName, CategoryDomainModel[]>> Handle(GetCategoriesRequest request, CancellationToken cancellationToken)
        {
            var categories = await GetCategoriesAsync();

            if (request.ModuleName.HasValue)
                categories = categories!.Where(w => w.Key == request.ModuleName.Value).ToDictionary(w => w.Key, w => w.Value);

            return await Task.FromResult(categories!);
        }
        
        /// <summary>
        /// Request handler for getting available services.
        /// </summary>
        /// <param name="request">Request for available services</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Available services</returns>
        public async Task<IEnumerable<ActionDomainModel>?> Handle(GetActionsRequest request, CancellationToken cancellationToken)
        {
            var categories = await GetCategoriesAsync();

            var response = categories!.SelectMany(sm => sm.Value).Where(filter => filter.CategoryCode == request.CategoryCode)
                .Select(s => s.Action).FirstOrDefault();

            return await Task.FromResult(response);
        }
        
        /// <summary>
        /// Request handler for getting available events by serviceId
        /// </summary>
        /// <param name="request">Request for available events by serviceId</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Available events</returns>
        public async Task<IDictionary<ModuleName, EventDomainModel[]>> Handle(GetEventsRequest request, CancellationToken cancellationToken)
        {
            var events = await GetServiceEventsAsync();
            
            if (request.ModuleName.HasValue)
                events = events!.Where(w => w.Key == request.ModuleName.Value).ToDictionary(w => w.Key, w => w.Value);

            return await Task.FromResult(events);
        }

        /// <summary>
        /// Method for getting all categories
        /// </summary>
        /// <returns>All categories</returns>
        private async Task< IDictionary<ModuleName, CategoryDomainModel[]>> GetCategoriesAsync()
        {
            var categories = JsonConvert.DeserializeObject<IDictionary<ModuleName, CategoryDomainModel[]>>(System.Text.Encoding.Default.GetString(JsonResource.ServiceCategories));
            if (categories == null)
                throw new FileNotFoundException("Not include data of categories.");

            return await Task.FromResult(categories);
        }
        
        /// <summary>
        /// Method for getting all events
        /// </summary>
        /// <returns>All events</returns>
        private async Task<IDictionary<ModuleName, EventDomainModel[]>> GetServiceEventsAsync()
        {
            var events = JsonConvert.DeserializeObject<IDictionary<ModuleName, EventDomainModel[]>>(System.Text.Encoding.Default.GetString(JsonResource.ServiceEvents));
            if (events == null)
                throw new FileNotFoundException("Not include data of events.");

            return await Task.FromResult(events);
        }
    }
}
