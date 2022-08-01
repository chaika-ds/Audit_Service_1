using System.Globalization;
using Xunit.Sdk;

namespace AuditService.Tests.AuditService.Handlers.Fakes;

/// <summary>
/// Fake values for testing PlayerChangesLogRequestHandler
/// </summary>
internal class FakeValues
{
    internal static DateTime Timestamp1 = DateTime.ParseExact("28/07/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture);
    internal static DateTime Timestamp2 = DateTime.ParseExact("29/07/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture);
    internal static Guid UserId1 = Guid.NewGuid();
    internal static Guid UserId2 = Guid.NewGuid();

    internal static string LocalizeValue1 = "LocalizeValue1";
    internal static string LocalizeValue2 = "LocalizeValue2";
    internal static string LocalizeValue3 = "LocalizeValue3";
    internal static string LocalizeValue4 = "LocalizeValue4";
    internal static string LocalizeValue5 = "LocalizeValue5";
    internal static string LocalizeValue6 = "LocalizeValue6";
    internal static string LocalizeValue7 = "LocalizeValue7";
    internal static string LocalizeValue8 = "LocalizeValue8";

    internal static string Type1 = "UserAttributeType1";
    internal static string Type2 = "UserAttributeType2";
    internal static string Type3 = "UserAttributeType3";
    internal static string Type4 = "UserAttributeType4";
    internal static string Type5 = "UserAttributeType5";
    internal static string Type6 = "UserAttributeType6";
    internal static string Type7 = "UserAttributeType7";
    internal static string Type8 = "UserAttributeType8";

    internal static string Value1 = "UserAttributeValue1";
    internal static string Value2 = "UserAttributeValue2";
    internal static string Value3 = "UserAttributeValue3";
    internal static string Value4 = "UserAttributeValue4";
    internal static string Value5 = "UserAttributeValue5";
    internal static string Value6 = "UserAttributeValue6";
    internal static string Value7 = "UserAttributeValue7";
    internal static string Value8 = "UserAttributeValue8";

    internal static string EventKey1 = "TestEventCode1";
    internal static string EventKey2 = "TestEventCode2";

    internal static string EventName1 = "TestEventName1";
    internal static string EventName2 = "TestEventName2";

    internal static string EventCode1 = "TestEventCode1";
    internal static string EventCode2 = "TestEventCode2";

    internal static string UserAgent1 = "TestEventCode1";
    internal static string UserAgent2 = "TestEventCode2";

    internal static string IpAddress1 = "000.000.000.000";
    internal static string IpAddress2 = "111.111.111.111";

    internal static string Reason1 = "TestReason1";
    internal static string Reason2 = "TestReason2";

    internal static string UserLogin1 = "test@test.email1";
    internal static string UserLogin2 = "test@test.email2";

    internal static string LanguageTest = "en";
}