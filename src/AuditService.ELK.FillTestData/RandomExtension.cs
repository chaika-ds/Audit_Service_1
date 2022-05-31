namespace AuditService.ELK.FillTestData;

/// <summary>
///     Класс для расширения рандомайзера
/// </summary>
internal static class RandomExtension
{
    /// <summary>
    ///     Получить случайный элемент коллекции
    /// </summary>
    /// <typeparam name="TItem">Тип элемента коллекции</typeparam>
    /// <param name="collection">Коллекция</param>
    /// <param name="random">Рандомайзер</param>
    public static TItem GetRandomItem<TItem>(this IList<TItem> collection, Random random)
    {
        var index = random.Next(collection.Count - 1);
        return collection[index];
    }

    /// <summary>
    ///     Получить случайную дату
    /// </summary>
    /// <param name="dateTime">Текущая дата</param>
    /// <param name="random">Рандомайзер</param>
    public static DateTime GetRandomItem(this DateTime dateTime, Random random)
    {
        var days = random.Next(29);
        var hours = random.Next(23);
        var minutes = random.Next(59);
        var seconds = random.Next(59);

        return dateTime.AddDays(-days).AddHours(-hours).AddMinutes(-minutes).AddSeconds(-seconds);
    }
}