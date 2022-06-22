using System.Security.Cryptography;
using System.Text;
using AuditService.Common.Enums;

namespace AuditService.Setup.Extensions;

public static class HashStringExtension
{
    /// <summary>
    /// Получить Хэш сумму
    /// </summary>
    public static string GetHash(this string text, HashType hashType)
    {
        if (string.IsNullOrEmpty(text))
            return "string.Empty";

        switch (hashType)
        {
            case HashType.MD5:
                return GetMd5(text);
            case HashType.SHA1:
                return GetSha1(text);
            case HashType.SHA256:
                return GetSha256(text);
            case HashType.SHA512:
                return GetSha512(text);
            default:
                return "Invalid Hash Type";
        }
    }

    /// <summary>
    /// Проверить Хэш сумму
    /// </summary>
    public static bool CheckHash(this string originalString, string hashString, HashType hashType)
    {
        return GetHash(originalString, hashType) == hashString;
    }

    private static string GetMd5(string text)
    {
        var encode = new UTF8Encoding();
        var message = encode.GetBytes(text);
        MD5 hashString = new MD5CryptoServiceProvider();
        var hashValue = hashString.ComputeHash(message);
        return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
    }

    private static string GetSha1(string text)
    {
        var encode = new UTF8Encoding();
        var message = encode.GetBytes(text);
        var hashString = new SHA1Managed();
        var hashValue = hashString.ComputeHash(message);
        return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
    }

    private static string GetSha256(string text)
    {
        var encode = new UTF8Encoding();
        var message = encode.GetBytes(text);
        var hashString = new SHA256Managed();
        var hashValue = hashString.ComputeHash(message);
        return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
    }

    private static string GetSha512(string text)
    {
        var encode = new UTF8Encoding();
        var message = encode.GetBytes(text);
        var hashString = new SHA512Managed();
        var hashValue = hashString.ComputeHash(message);
        return hashValue.Aggregate("", (current, x) => current + $"{x:x2}");
    }
}