using System;

namespace AuditService.EventConsumerApp.Auth
{
    public interface IPermissionPusherSettings
    {
        public string Topic { get; }

        public Guid ServiceId { get; set; }

        public string ServiceName { get; set; }
    }
}
