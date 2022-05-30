using AuditService.Common.Args;
using AuditService.Common.Health;
using AuditService.Common.Kafka;
using AuditService.Common.Services;
using AuditService.Common.Services.ExternalConnectionServices;
using AuditService.Data.Domain.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace AuditService.EventConsumerApp
{
    public class InputAuditServiceTransactions :  BaseInputService<AuditLogMessageDto>
    {
        public InputAuditServiceTransactions(
            ILogger<InputAuditServiceTransactions> logger,
            IKafkaConsumerFactory consumerFactory,
            IInputSettings<AuditLogMessageDto> inputSettings,
            IHealthMarkService healthService)
                : base(logger, consumerFactory, inputSettings, healthService)
        {
        }

        protected override async Task OnMessageReceivedAsync(object sender, MessageReceivedArgumentEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Data))
            {
                return;
            }

            try
            {
                var inputObject = JsonConvert.DeserializeObject<AuditLogMessageDto>(args.Data);

                await CreateAndSaveAsync(inputObject).ConfigureAwait(false);                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing {typeof(AuditLogMessageDto).Name}");
            }
        }

        protected Task CreateAndSaveAsync(AuditLogMessageDto inputObject)
        {
            //return _outputService.MapToEntityAndSaveAsync<PaymentTransactionDto, SuccessDeposit>(inputObject);

            return null;
        }
    }
}
