﻿namespace AuditService.Kafka.Exceptions;

public class KafkaConsumerException : Exception
{
    public KafkaConsumerException(string message)
        : base(message)
    {
    }
}
