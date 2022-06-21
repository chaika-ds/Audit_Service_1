namespace AuditService.Common.Models.Dto
{
    /// <summary>
    /// Permissions to Kafka
    /// </summary>
    public class PermissionsKafkaDto
    {
        public int MessageType { get; set; }

        public Guid ServiceId { get; set; }

        public string Group { get; set; }

        public string Action { get; set; }

        public string Description { get; set; }

    }
}
