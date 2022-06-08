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
    /// <summary>
    /// Service for consuming Audit log messages
    /// </summary>
    public class InputAuditServiceTransactions :  BaseInputService<AuditLogTransactionDto>
    {
        public InputAuditServiceTransactions(
            ILogger<InputAuditServiceTransactions> logger,
            IKafkaConsumerFactory consumerFactory,
            IInputSettings<AuditLogTransactionDto> inputSettings,
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
                var inputObject = JsonConvert.DeserializeObject<AuditLogTransactionDto>(args.Data);

                //await CreateAndSaveAsync(inputObject).ConfigureAwait(false);
                await Task.Delay(1);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing {typeof(AuditLogTransactionDto).Name}");
            }
        }

        //protected Task CreateAndSaveAsync(AuditLogTransactionDto inputObject)
        //{
        //    //return _outputService.MapToEntityAndSaveAsync<PaymentTransactionDto, SuccessDeposit>(inputObject);

        //    return new Task();
        //}
    }
}
