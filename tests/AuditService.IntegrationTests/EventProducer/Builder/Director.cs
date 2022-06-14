using System;
using System.Linq;
using System.Threading.Tasks;
using AuditService.Kafka.Kafka;
using Microsoft.Extensions.DependencyInjection;

namespace AuditService.IntegrationTests.EventProducer.Builder
{
    public class Director : IDirector
    {
        private readonly IServiceProvider _services;
        private readonly KafkaProducer _producer;
        private readonly IDirectorSettings _settings;

        public Director(IServiceProvider services, KafkaProducer producer, IDirectorSettings settings)
        {
            _services = services;
            _producer = producer;
            _settings = settings;
        }

        public async Task GenerateDtoAsync<T>(int count = 1)
            where T : class
        {
            using var scope = _services.CreateScope();
            var builder = scope?.ServiceProvider?.GetService<IBuilderDto<T>>();

            var cc = builder?.Get();

            var msgTasks = Enumerable.Range(0, count)
                .Select(async x => await PushAsync(builder.Get()))
                .ToArray();
           await Task.WhenAll(msgTasks);            
        }

        public async Task<T> SendDtoAsync<T>(T dto) where T : class
        {
            await PushAsync(dto);
            return dto;
        }

        private async Task PushAsync<T>(T dto) where T : class
            => await _producer.SendAsync(dto, _settings.Topics[typeof(T).Name]);
    }
}
