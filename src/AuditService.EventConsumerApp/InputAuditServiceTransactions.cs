using AuditService.Kafka.Args;
using AuditService.Kafka.Kafka;
using AuditService.Kafka.Services;
using AuditService.Kafka.Services.ExternalConnectionServices;
using AuditService.Data.Domain.Dto;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using AuditService.Kafka.Services.Health;
using Tolar.Kafka;

namespace AuditService.EventConsumerApp
{
    /// <summary>
    /// Service for consuming Audit log messages
    /// </summary>
    public class InputAuditServiceTransactions :  BaseInputService<AuditLogTransactionDomainModel>
    {
        public InputAuditServiceTransactions(
            ILogger<InputAuditServiceTransactions> logger,
            IKafkaConsumerFactory consumerFactory,
            IInputSettings<AuditLogTransactionDomainModel> inputSettings,
            IHealthMarkService healthService)
                : base(logger, consumerFactory, inputSettings, healthService)
        {
        }

        protected override async Task OnMessageReceivedAsync(object sender, MessageReceivedEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Data))
            {
                return;
            }

            try
            {
                var inputObject = JsonConvert.DeserializeObject<AuditLogTransactionDomainModel>(args.Data);

                //await CreateAndSaveAsync(inputObject).ConfigureAwait(false);
                await Task.Delay(1);
                return;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing {typeof(AuditLogTransactionDomainModel).Name}");
            }
        }

        //protected Task CreateAndSaveAsync(AuditLogTransactionDomainModel inputObject)
        //{
        //    //return _outputService.MapToEntityAndSaveAsync<PaymentTransactionDto, SuccessDeposit>(inputObject);

        //    return new Task();
        //}
    }
}
