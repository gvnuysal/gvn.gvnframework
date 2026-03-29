namespace Gvn.GvnFramework.Core.Extensions;

/// <summary>
/// Provides extension methods for <see cref="IEnumerable{T}"/> sequences.
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// Returns <c>true</c> if the sequence is <c>null</c> or contains no elements.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="source">The sequence to evaluate.</param>
    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        => source is null || !source.Any();

    /// <summary>
    /// Filters out all <c>null</c> elements from the sequence.
    /// </summary>
    /// <typeparam name="T">The non-nullable element type.</typeparam>
    /// <param name="source">The sequence that may contain null elements.</param>
    /// <returns>A sequence containing only the non-null elements.</returns>
    public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T?> source) where T : class
        => source.Where(x => x is not null)!;

    /// <summary>
    /// Splits the sequence into consecutive batches of the specified size.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    /// <param name="source">The sequence to split.</param>
    /// <param name="size">The maximum number of elements per batch.</param>
    /// <returns>An enumerable of batches, each containing at most <paramref name="size"/> elements.</returns>
    public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
    {
        var batch = new List<T>(size);
        foreach (var item in source)
        {
            batch.Add(item);
            if (batch.Count == size)
            {
                yield return batch;
                batch = new List<T>(size);
            }
        }

        if (batch.Count > 0)
            yield return batch;
    }
}
