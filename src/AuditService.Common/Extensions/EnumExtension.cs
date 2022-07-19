using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using AuditService.Common.Attributes;

namespace AuditService.Common.Extensions;

/// <summary>
///     Functions for enum
/// </summary>
public static class EnumExtension
{
    /// <summary>
    ///     Get description from Display\Description attribute
    /// </summary>
    /// <param name="enum">Object enum</param>
    public static string? Description<TEnum>(this TEnum @enum) where TEnum : Enum
    {
        var fieldInfo = GetFieldInfo(@enum);

        if (fieldInfo is null)
            return null;

        var descriptionAttribute =
            ((DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false))
            .FirstOrDefault();
        if (descriptionAttribute != null)
            return descriptionAttribute.Description;

        var displayAttribute = ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false))
            .FirstOrDefault();

        return displayAttribute == null ? @enum.ToString() : displayAttribute.GetName();
    }
    
    /// <summary>
    ///     Get localization key
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <param name="enum">Enum object</param>
    /// <returns>Localization key</returns>
    public static string LocalizationKey<TEnum>(this TEnum @enum) where TEnum : Enum
    {
        var fieldInfo = GetFieldInfo(@enum);

        if (fieldInfo is null)
            return string.Empty;

        if (fieldInfo.GetCustomAttributes(typeof(LocalizationAttribute), false).FirstOrDefault() is LocalizationAttribute localizationAttribute)
            return localizationAttribute.Key;

        return @enum.ToString();
    }

    /// <summary>
    /// Get field info to enum
    /// </summary>
    /// <typeparam name="TEnum">Enum type</typeparam>
    /// <param name="enum">Enum object</param>
    /// <returns>FieldInfo</returns>
    private static FieldInfo? GetFieldInfo<TEnum>(this TEnum @enum) where TEnum : Enum
    {
        var name = @enum.ToString();

        if (string.IsNullOrEmpty(name))
            return null;

        var fieldInfo = @enum.GetType().GetField(name);

        return fieldInfo;
    }
}