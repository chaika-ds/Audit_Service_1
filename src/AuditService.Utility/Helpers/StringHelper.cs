using System.Security.Cryptography;
using System.Text;

namespace AuditService.Utility.Helpers;

public static class StringHelper
{
    public static string GetCheckSum(string value)
    {
        using var md5 = MD5.Create();

        var computedHash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

        return BitConverter.ToString(computedHash).Replace("-", string.Empty);
    }
}