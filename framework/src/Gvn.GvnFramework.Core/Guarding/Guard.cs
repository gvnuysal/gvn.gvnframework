namespace Gvn.GvnFramework.Core.Guarding;

/// <summary>
/// Provides static guard clause methods to validate arguments and state,
/// throwing appropriate exceptions when conditions are not met.
/// </summary>
public static class Guard
{
    /// <summary>
    /// Ensures the given value is not null.
    /// </summary>
    /// <typeparam name="T">The reference type to check.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="name">The name of the argument, used in the exception message.</param>
    /// <returns>The non-null value.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    public static T NotNull<T>(T? value, string name) where T : class
        => value ?? throw new ArgumentNullException(name);

    /// <summary>
    /// Ensures the given string is not null or whitespace.
    /// </summary>
    /// <param name="value">The string value to check.</param>
    /// <param name="name">The name of the argument, used in the exception message.</param>
    /// <returns>The non-null, non-whitespace string value.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is null or whitespace.</exception>
    public static string NotNullOrWhiteSpace(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{name} cannot be null or whitespace.", name);

        return value;
    }

    /// <summary>
    /// Ensures the given condition is true.
    /// </summary>
    /// <param name="condition">The condition to evaluate.</param>
    /// <param name="message">The message used in the exception when the condition is false.</param>
    /// <exception cref="InvalidOperationException">Thrown when <paramref name="condition"/> is false.</exception>
    public static void True(bool condition, string message)
    {
        if (!condition)
            throw new InvalidOperationException(message);
    }

    /// <summary>
    /// Ensures the given collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <param name="value">The collection to check.</param>
    /// <param name="name">The name of the argument, used in the exception message.</param>
    /// <returns>The non-null, non-empty collection.</returns>
    /// <exception cref="ArgumentException">Thrown when <paramref name="value"/> is null or empty.</exception>
    public static IEnumerable<T> NotEmpty<T>(IEnumerable<T>? value, string name)
    {
        if (value is null || !value.Any())
            throw new ArgumentException($"{name} cannot be null or empty.", name);

        return value;
    }

    /// <summary>
    /// Ensures the given value is within the specified inclusive range.
    /// </summary>
    /// <typeparam name="T">A comparable type.</typeparam>
    /// <param name="value">The value to check.</param>
    /// <param name="min">The minimum allowed value (inclusive).</param>
    /// <param name="max">The maximum allowed value (inclusive).</param>
    /// <param name="name">The name of the argument, used in the exception message.</param>
    /// <returns>The value if it is within range.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown when <paramref name="value"/> is outside the range.</exception>
    public static T InRange<T>(T value, T min, T max, string name) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new ArgumentOutOfRangeException(name, $"{name} must be between {min} and {max}.");

        return value;
    }
}
