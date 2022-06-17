namespace AuditService.ELK.FillTestData;

/// <summary>
///     Extension class for random fuction
/// </summary>
internal static class RandomExtension
{
    /// <summary>
    ///     Get random item from collection
    /// </summary>
    /// <typeparam name="TItem">Type item of colletion</typeparam>
    /// <param name="collection">Collection</param>
    /// <param name="random">Random fuction</param>
    public static TItem GetRandomItem<TItem>(this IList<TItem> collection, Random random)
    {
        var index = random.Next(collection.Count - 1);
        return collection[index];
    }

    /// <summary>
    ///     Get random date & time
    /// </summary>
    /// <param name="dateTime">Current date</param>
    /// <param name="random">Random fuction</param>
    public static DateTime GetRandomItem(this DateTime dateTime, Random random)
    {
        var days = random.Next(29);
        var hours = random.Next(23);
        var minutes = random.Next(59);
        var seconds = random.Next(59);

        return dateTime.AddDays(-days).AddHours(-hours).AddMinutes(-minutes).AddSeconds(-seconds);
    }
}