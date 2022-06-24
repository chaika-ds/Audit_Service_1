using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace AuditService.Setup.Extensions;

    /// <summary>
    ///     Доп. функции для перечислений
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        ///     Получить описание с аттрибута Display\Description
        /// </summary>
        /// <param name="enum">Объект перечисления</param>
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
        ///     Проверить на наличие аттрибута
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления</typeparam>
        /// <param name="enum">Объект перечисления</param>
        /// <param name="attributeType">Тип искомого аттрибута</param>
        public static bool HasAttribute<TEnum>(this TEnum @enum, Type attributeType)
        {
            return (bool) @enum?.GetType().GetField(@enum.ToString()).GetCustomAttributes(attributeType, false).Any();
        }

        /// <summary>
        ///     Вывести строкой. Высокий регистр букв
        /// </summary>
        /// <param name="enum">Объект перечисления</param>
        public static string? ToUpperString(this Enum @enum)
        {
            return @enum?.ToString()?.ToUpper();
        }

        /// <summary>
        ///     Вывести строкой. Низкий регистр букв
        /// </summary>
        /// <param name="enum">Объект перечисления</param>
        public static string? ToLowerString(this Enum @enum)
        {
            return @enum?.ToString()?.ToLower();
        }

        /// <summary>
        ///     Конвертировать объект в ENUM перечисление
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления</typeparam>
        /// <param name="value">Значение объекта</param>
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
        ///     Конвертировать перечисление в INT значение
        /// </summary>
        /// <param name="enum">Объект перечисления</param>
        public static int ToInt(this Enum @enum)
        {
            return Convert.ToInt32(@enum);
        }
    }