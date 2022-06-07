﻿using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace AuditService.Common;

public static class Helper
{
    /// <summary>
    /// 
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

