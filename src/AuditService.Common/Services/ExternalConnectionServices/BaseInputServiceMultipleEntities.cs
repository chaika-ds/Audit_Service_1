

using AuditService.Common.Args;
using AuditService.Common.Health;
using AuditService.Common.Kafka;
using bgTeam;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuditService.Common.Services.ExternalConnectionServices
{
    public abstract class BaseInputServiceMultipleEntities<TInput, TOutputType> : BaseInputService<TInput>
       where TInput : class, new()
       where TOutputType : struct
    {
        protected readonly TOutputType[] _typesArray;

        protected BaseInputServiceMultipleEntities(
            ILogger logger,
            IKafkaConsumerFactory consumerFactory,
            IInputSettings<TInput> inputSettings,
            IHealthMarkService healthService)
            : base(logger, consumerFactory, inputSettings, healthService)
        {
            var type = typeof(TOutputType);
            if (!type.IsEnum)
            {
                throw new NotSupportedException(type.Name);
            }

            _typesArray = Enum.GetValues(type)
                .Cast<TOutputType>()
                .ToArray();
        }

        protected override async Task OnMessageReceivedAsync(object sender, MessageReceivedArgumentEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Data))
            {
                return;
            }

            try
            {
                var inputObject = JsonConvert.DeserializeObject<TInput>(args.Data);
                if (IsShouldSave(inputObject))
                {
                    await CreateAndSaveAsync(inputObject).ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing {typeof(TInput).Name} ({typeof(TOutputType).Name})");
            }
        }

        protected abstract Task CreateAndSaveAsync(TInput inputObject);

        protected abstract bool IsShouldSave(TInput inputObject);
    }
}
