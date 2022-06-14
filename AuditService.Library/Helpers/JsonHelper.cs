using Newtonsoft.Json;

namespace AuditService.Utility.Helpers;

public static class JsonHelper
{
    /// <summary>
    /// Get JSON in string format from file
    /// </summary>
    /// <param name="jsonAdress"></param>
    /// <returns></returns>
    public static string GetJson(string jsonAdress)
    {
        using (StreamReader r = new StreamReader(jsonAdress))
        {
            var json = r.ReadToEnd();
            return json;
        }
    }

    /// <summary>
    /// Serialize from model T to JSON in string format
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="incomeObj"></param>
    /// <returns></returns>
    public static string SerializeToString<T>(T incomeObj) where T : class
    {
        var _serializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            Converters = new List<JsonConverter>() { new Newtonsoft.Json.Converters.StringEnumConverter() }
        };

        var objStr = JsonConvert.SerializeObject(incomeObj, _serializerSettings);

        return objStr;
    }
}

