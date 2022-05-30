﻿namespace AuditService.Common.Kafka
{
    public interface IKafkaConsumerSettings
    {
        int MaxTimeoutMsec { get; set; }

        int MaxThreadsCount { get; set; }

        Dictionary<string, string> Config { get; set; }
    }

}