using AuditService.Data.Domain.Enums;

namespace AuditService.Common.Logger
{
    /// <summary>
    /// Parsing log channels from environment
    /// </summary>
    public static class LogChannelParsing
    {
        public static LogChannel CheckAndParseChannel(string environmentName)
        {
            LogChannel name;

            if (Enum.TryParse(environmentName, out name))
                switch (name.GetHashCode())
                {
                    case 0:
                        return LogChannel.uat;
                    case 1:
                        return LogChannel.development;
                    case 2:
                        return LogChannel.test;
                    case 3:
                        return LogChannel.demo;
                    default:
                        return LogChannel.production;
                }
            else return LogChannel.wrongChannel;
        }
    }
}
