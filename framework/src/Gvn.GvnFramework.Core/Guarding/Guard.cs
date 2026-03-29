namespace Gvn.GvnFramework.Core.Guarding;

public static class Guard
{
    public static T NotNull<T>(T? value, string name) where T : class
        => value ?? throw new ArgumentNullException(name);

    public static string NotNullOrWhiteSpace(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException($"{name} cannot be null or whitespace.", name);

        return value;
    }

    public static void True(bool condition, string message)
    {
        if (!condition)
            throw new InvalidOperationException(message);
    }

    public static IEnumerable<T> NotEmpty<T>(IEnumerable<T>? value, string name)
    {
        if (value is null || !value.Any())
            throw new ArgumentException($"{name} cannot be null or empty.", name);

        return value;
    }

    public static T InRange<T>(T value, T min, T max, string name) where T : IComparable<T>
    {
        if (value.CompareTo(min) < 0 || value.CompareTo(max) > 0)
            throw new ArgumentOutOfRangeException(name, $"{name} must be between {min} and {max}.");

        return value;
    }
}