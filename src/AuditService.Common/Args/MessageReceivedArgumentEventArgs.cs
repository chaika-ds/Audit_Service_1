namespace AuditService.Common.Args
{
    public class MessageReceivedArgumentEventArgs : EventArgs
    {
        public MessageReceivedArgumentEventArgs(long offset, string key, string data, DateTime timestamp)
        {
            Offset = offset;
            Data = data;
            Timestamp = timestamp;
            Key = key;
        }

        public long Offset { get; }

        public string Key { get; }

        public string Data { get; }

        public DateTime Timestamp { get; }

        public override string ToString()
        {
            return $"Offset: {Offset}, Data: {Data}";
        }
    }
}
