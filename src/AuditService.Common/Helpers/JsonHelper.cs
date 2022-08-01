using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AuditService.Common.Helpers;

public static class JsonHelper
{
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

    /// <summary>
    /// Convert an object to a byte array
    /// </summary>
    /// <param name="obj">Object for converting</param>
    /// <returns>Byte array</returns>
    public static byte[] ObjectToByteArray(Object obj)
    {
        var jsonObject = SerializeToString(obj);
        return Encoding.UTF8.GetBytes(jsonObject);
    }
}