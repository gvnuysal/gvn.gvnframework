namespace Gvn.GvnFramework.Core.Extensions;

/// <summary>
/// Provides extension methods for <see cref="string"/> values.
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Returns <c>true</c> if the string is <c>null</c>, empty, or consists only of whitespace characters.
    /// </summary>
    /// <param name="value">The string to evaluate.</param>
    public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Returns <c>true</c> if the string is not <c>null</c>, not empty, and contains at least one non-whitespace character.
    /// </summary>
    /// <param name="value">The string to evaluate.</param>
    public static bool HasValue(this string? value) => !string.IsNullOrWhiteSpace(value);

    /// <summary>
    /// Converts a PascalCase or camelCase string to snake_case.
    /// </summary>
    /// <param name="value">The string to convert.</param>
    /// <returns>The snake_case representation of the input string.</returns>
    public static string ToSnakeCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return string.Concat(
            value.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + c : c.ToString())
        ).ToLower();
    }

    /// <summary>
    /// Truncates the string to the specified maximum length.
    /// Returns the original string if it is shorter than or equal to <paramref name="maxLength"/>.
    /// </summary>
    /// <param name="value">The string to truncate.</param>
    /// <param name="maxLength">The maximum number of characters to keep.</param>
    /// <returns>A string of at most <paramref name="maxLength"/> characters.</returns>
    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value;

        return value[..maxLength];
    }
}
