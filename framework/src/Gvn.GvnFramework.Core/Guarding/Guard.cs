namespace Gvn.GvnFramework.Core.Guarding;

public static class Guard
{
    public static T NotNull<T>(T? value,string name) where T:class=> value ?? throw new ArgumentNullException(name);

    public static string NotNulOrWhiteSpace(string? value, string name)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException($"{name} can not be empty", name);
        }
        
        return value;
    }
    public static void True(bool condition, string message)
    {
        if (!condition)
        {
            throw new  InvalidOperationException(message);
        }
    }
}