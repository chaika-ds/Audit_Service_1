﻿using AuditService.Common.Enums;
using AuditService.Common.Extensions;
using AuditService.Common.Models.Domain;
using AuditService.Common.Models.Dto;
using AuditService.Common.Resources;
using MediatR;
using Newtonsoft.Json;

namespace AuditService.Handlers.Handlers
{
    /// <summary>
    /// Handler for a request to receive reference resources (services\categories)
    /// </summary>
    public class ReferenceRequestHandler : IRequestHandler<GetServicesRequest, IEnumerable<EnumResponseDto>>,
        IRequestHandler<GetCategoriesRequest, IDictionary<ServiceStructure, CategoryDomainModel[]>>
    {
        /// <summary>
        /// Request handler for getting available services.
        /// </summary>
        /// <param name="request">Request for available services</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Available services</returns>
        public Task<IEnumerable<EnumResponseDto>> Handle(GetServicesRequest request,
            CancellationToken cancellationToken)
        {
            var result = Enum.GetValues<ServiceStructure>().Select(value => new EnumResponseDto(value.ToString(), value.Description()));
            return Task.FromResult(result);
        }

        /// <summary>
        /// Request handler for getting available categories by serviceId
        /// </summary>
        /// <param name="request">Request for available categories by serviceId</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Available categories</returns>
        public async Task<IDictionary<ServiceStructure, CategoryDomainModel[]>> Handle(GetCategoriesRequest request,
            CancellationToken cancellationToken)
        {
            var categories = JsonConvert.DeserializeObject<IDictionary<ServiceStructure, CategoryDomainModel[]>>(System.Text.Encoding.Default.GetString(JsonResource.ServiceCategories));
            if (categories == null)
                throw new FileNotFoundException("Not include data of categories.");

            if (request.ServiceId.HasValue)
                categories = categories.Where(w => w.Key == request.ServiceId.Value).ToDictionary(w => w.Key, w => w.Value);

            return await Task.FromResult(categories);
        }
    }
}