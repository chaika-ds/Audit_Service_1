namespace AuditService.Common.Extensions;

/// <summary>
///     Extension for working with
/// </summary>
public static class EnumerableExtension
{
    /// <summary>
    ///     Select many items asynchronously.
    ///     Use consciously, parallelization of tasks is performed!
    /// </summary>
    /// <typeparam name="T">IEnumerable type to select</typeparam>
    /// <typeparam name="T1">IEnumerable type to be selected</typeparam>
    /// <param name="enumeration">IEnumerable to select</param>
    /// <param name="func">Function to select</param>
    /// <returns>Selected IEnumerable</returns>
    public static async Task<IEnumerable<T1>> SelectManyAsync<T, T1>(this IEnumerable<T> enumeration, Func<T, Task<IEnumerable<T1>>> func)
        => (await Task.WhenAll(enumeration.Select(func))).SelectMany(s => s);
}