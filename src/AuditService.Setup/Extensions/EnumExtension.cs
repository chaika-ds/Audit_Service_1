using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AuditService.Setup.Extensions;

    /// <summary>
    ///     Functions for enum
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        ///     Get description from Display\Description attribute
        /// </summary>
        /// <param name="enum">Object enum</param>
        public static string? Description<TEnum>(this TEnum @enum)
        {
            if (@enum == null)
                return string.Empty;

            var fieldInfo = @enum.GetType().GetField(@enum.ToString());

            var descriptionAttribute = ((DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)).FirstOrDefault();
            if (descriptionAttribute != null)
                return descriptionAttribute.Description;

            var displayAttribute = ((DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false)).FirstOrDefault();
            
            return displayAttribute == null ? @enum.ToString() : displayAttribute.GetName();
        }

        /// <summary>
        ///     Check for presence of an attribute
        /// </summary>
        /// <typeparam name="TEnum">Enum Type</typeparam>
        /// <param name="enum">Enum Object</param>
        /// <param name="attributeType">Attribute type to search</param>
        public static bool HasAttribute<TEnum>(this TEnum @enum, Type attributeType)
        {
            return (bool) @enum?.GetType().GetField(@enum.ToString()).GetCustomAttributes(attributeType, false).Any();
        }

        /// <summary>
        ///     Output as a string. High case letters
        /// </summary>
        /// <param name="enum">Enum object</param>
        public static string? ToUpperString(this Enum @enum)
        {
            return @enum?.ToString()?.ToUpper();
        }

        /// <summary>
        ///     Output as a string. Low case letters
        /// </summary>
        /// <param name="enum">Enum object</param>
        public static string? ToLowerString(this Enum @enum)
        {
            return @enum?.ToString()?.ToLower();
        }

        /// <summary>
        ///     Convert object to ENUM enum
        /// </summary>
        /// <typeparam name="TEnum">Enum type</typeparam>
        /// <param name="value">Object value</param>
        public static TEnum ToEnum<TEnum>(this object value) where TEnum : struct
        {
            try
            {
                var res = (TEnum)Enum.Parse(typeof(TEnum), value.ToString());
                return !Enum.IsDefined(typeof(TEnum), res) ? default : res;
            }
            catch
            {
                return default;
            }
        }

        /// <summary>
        ///     Convert enum to int value
        /// </summary>
        /// <param name="enum">Enum object</param>
        public static int ToInt(this Enum @enum)
        {
            return Convert.ToInt32(@enum);
        }
    }