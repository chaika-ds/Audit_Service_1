using AuditService.Common.Enums;

namespace AuditService.Utility.Helpers;

public static class EnumHelper
{
    /// <summary>
    /// Parsing log channels from environment
    /// </summary>
    /// <param name="environmentName"></param>
    /// <returns></returns>
    public static LogChannel CheckAndParseChannel(string environmentName)
    {
        LogChannel name;

        if (Enum.TryParse(environmentName, out name))
            switch ((int)name)
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
