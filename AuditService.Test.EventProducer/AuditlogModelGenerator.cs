using AuditService.Common.Kafka;
using AuditService.Data.Domain.Dto;

namespace AuditService.Test.EventProducer
{
    public class AuditlogModelGenerator
    {
        private readonly KafkaProducer _producer;

        public AuditlogModelGenerator(KafkaProducer producer)
        {
            _producer = producer;
        }

        public async Task SendToAuditAsync(HttpContext context)
        {
            if (context.Request.Path == "/_hc")
            {
                return;
            }

            try
            {
                var body = await GetRequestBodyStringAsync(context.Request);

                var project = JsonConvert.DeserializeAnonymousType(body, new { ProjectId = Guid.Empty });
                if (project == null)
                {
                    return;
                }

                await _producer.SendAsync(
                    new AuditLogMessageDto
                    {
                        ServiceName = "REPORTS",
                        NodeId = project.ProjectId,
                        NodeType = "PROJECT",
                        RequestBody = body,
                        RequestUrl = $"{context.Request.Method}: {context.Request.Path}",
                        ActionName = context.Request.Path.Value.Contains("export", StringComparison.OrdinalIgnoreCase) ? "EXPORT" : "VIEW",
                        Timestamp = DateTime.UtcNow,
                        EntityName = GetCategory(context),
                    },
                    _settings.Topic);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Failed to send message in audit log");
            }
        }
    }
}
