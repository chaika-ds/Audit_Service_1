namespace AuditService.Common.Extensions;

/// <summary>
///     Extension for working with strings
/// </summary>
public static class StringExtension
{
    /// <summary>
    ///     Convert string to format camelCase
    /// </summary>
    /// <param name="value">String value to convert</param>
    /// <returns>Converted string value</returns>
    public static string ToCamelCase(this string value) => char.ToLowerInvariant(value[0]) + value[1..];
}