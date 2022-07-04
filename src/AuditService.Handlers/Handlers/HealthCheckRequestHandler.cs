using AuditService.Common.Models.Dto;
using AuditService.Kafka.AppSetings;
using AuditService.Kafka.Services.Health;
using MediatR;
using Nest;

namespace AuditService.Handlers.Handlers
{
    /// <summary>
    /// Service health check request handler
    /// </summary>
    public class HealthCheckRequestHandler : IRequestHandler<CheckElkHealthRequest, bool>,
        IRequestHandler<CheckKafkaHealthRequest, bool>
    {
        private readonly IHealthSettings _settings;
        private readonly IHealthService _healthService;
        private readonly IElasticClient _elasticClient;

        public HealthCheckRequestHandler(IHealthSettings settings, IHealthService healthService, IElasticClient elasticClient)
        {
            _settings = settings;
            _healthService = healthService;
            _elasticClient = elasticClient;
        }

        /// <summary>
        /// Handle a request for a health check of the Elk service
        /// </summary>
        /// <param name="request">Elk service health check request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Service health check result</returns>
        public async Task<bool> Handle(CheckElkHealthRequest request, CancellationToken cancellationToken) 
            => (await _elasticClient.Cluster.HealthAsync(ct: cancellationToken)).ApiCall.Success;

        /// <summary>
        /// Handle a request for a health check of the Kafka service
        /// </summary>
        /// <param name="request">Kafka service health check request</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Service health check result</returns>
        public Task<bool> Handle(CheckKafkaHealthRequest request, CancellationToken cancellationToken) 
            => Task.FromResult(_healthService.GetErrorsCount() < _settings.CriticalErrorsCount);
    }
}