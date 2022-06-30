using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AuditService.Utility.Helpers;

public static class JsonHelper
{
    /// <summary>
    ///     Get JSON in string format from file
    /// </summary>
    public static string GetJson(string jsonAddress)
    {
        using var r = new StreamReader(jsonAddress);
        return r.ReadToEnd();
    }

    /// <summary>
    ///     Serialize from model T to JSON in string format
    /// </summary>
    public static string SerializeToString<T>(this T obj) where T : class
    {
        return JsonConvert.SerializeObject(obj, new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            Converters = new List<JsonConverter> { new StringEnumConverter() }
        });
    }
}