namespace Gvn.GvnFramework.Core.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrWhiteSpace(this string? value) => string.IsNullOrWhiteSpace(value);

    public static bool HasValue(this string? value) => !string.IsNullOrWhiteSpace(value);

    public static string ToSnakeCase(this string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return value;

        return string.Concat(
            value.Select((c, i) => i > 0 && char.IsUpper(c) ? "_" + c : c.ToString())
        ).ToLower();
    }

    public static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value) || value.Length <= maxLength)
            return value;

        return value[..maxLength];
    }
}
