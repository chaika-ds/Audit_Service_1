namespace AuditService.Common.Extensions;

/// <summary>
/// DateTime extension
/// </summary>
public static class DateTimeExtension
{
    /// <summary>
    ///     Determine difference in months for dates
    /// </summary>
    /// <param name="firstDate">First DateTime</param>
    /// <param name="secondDate">Second DateTime</param>
    /// <returns>Difference in months</returns>
    public static int DetermineMonthDifference(this DateTime firstDate, DateTime secondDate) =>
        Math.Abs(secondDate.Month - firstDate.Month + 12 * (secondDate.Year - firstDate.Year));

    /// <summary>
    ///     Get time intervals of dates by month
    /// </summary>
    /// <param name="firstDate">First DateTime</param>
    /// <param name="secondDate">Second DateTime</param>
    /// <returns>Time intervals of dates by months</returns>
    public static IEnumerable<DateTime> GetTimeIntervalsOfDatesByMonth(this DateTime firstDate, DateTime secondDate) =>
        Enumerable.Range(0, 1 + firstDate.DetermineMonthDifference(secondDate))
            .Select(firstDate.AddMonths);

    /// <summary>
    ///     Convert date to elastic index format
    /// </summary>
    /// <param name="dateTime">DateTime to convert</param>
    /// <param name="elasticIndex">Elastic index</param>
    /// <returns>Elastic index with date</returns>
    public static string ToElasticIndexFormat(this DateTime dateTime, string? elasticIndex) =>
        $"{elasticIndex}-{dateTime:yyyy.MM}";
}