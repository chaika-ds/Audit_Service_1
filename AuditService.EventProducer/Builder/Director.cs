using AuditService.Common.Kafka;

namespace AuditService.EventProducer
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

        public async Task<T> GenerateDto<T>(int count = 1)
            where T : class
        {
            using var scope = _services.CreateScope();
            var builder = scope.ServiceProvider.GetService<IBuilderDto<T>>();

            var msgTasks = Enumerable.Range(0, count)
                .Select(x => Push(builder.Get()))
                .ToArray();
            await Task.WhenAll(msgTasks);

            return null;
        }

        public async Task<T> SendDto<T>(T dto) where T : class
        {
            await Push(dto);
            return dto;
        }

        private Task Push<T>(T dto) where T : class
            => _producer.SendAsync(dto, _settings.Topics[typeof(T).Name]);
    }
}
