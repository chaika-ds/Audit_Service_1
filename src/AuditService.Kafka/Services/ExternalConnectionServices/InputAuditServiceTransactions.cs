using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Tolar.Kafka;

namespace AuditService.Kafka.Services.ExternalConnectionServices
{
    /// <summary>
    /// Service for consuming Audit log messages
    /// </summary>
    public class InputAuditServiceTransactions : BaseInputService
    {
        public InputAuditServiceTransactions(
            ILogger<InputAuditServiceTransactions> logger,
            IKafkaConsumerFactory consumerFactory)
                : base(logger, consumerFactory)
        {
        }

        protected override async Task OnMessageReceivedAsync(object sender, MessageReceivedEventArgs args)
        {
            await Task.Delay(1);

            // TODO надо будет все это сделать
            //if (string.IsNullOrWhiteSpace(args.Data))
            //{
            //    return;
            //}

            //try
            //{
            //    var inputObject = JsonConvert.DeserializeObject<AuditLogTransactionDomainModel>(args.Data);

            //    //await CreateAndSaveAsync(inputObject).ConfigureAwait(false);
            //    await Task.Delay(1);
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, $"Error processing {typeof(AuditLogTransactionDomainModel).Name}");
            //}
        }

        //protected Task CreateAndSaveAsync(AuditLogTransactionDomainModel inputObject)
        //{
        //    //return _outputService.MapToEntityAndSaveAsync<PaymentTransactionDto, SuccessDeposit>(inputObject);

        //    return new Task();
        //}
    }
}
