using System.Runtime.Serialization;

namespace AuditService.Common.Exceptions;

/// <summary>
///     Exception received from kafka consumers
/// </summary>
[Serializable]
public class KafkaConsumerException : Exception
{
    /// <inheritdoc />
    public KafkaConsumerException()
    {
    }

    /// <inheritdoc />
    public KafkaConsumerException(string message) : base(message)
    {
    }

    /// <inheritdoc />
    public KafkaConsumerException(string message, Exception innerException) : base(message, innerException)
    {
    }

    /// <summary>
    ///     Constructor to ensure correct serialization/deserialization
    /// </summary>
    /// <param name="info">Data to serialize</param>
    /// <param name="context">Serialization thread context</param>
    protected KafkaConsumerException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}