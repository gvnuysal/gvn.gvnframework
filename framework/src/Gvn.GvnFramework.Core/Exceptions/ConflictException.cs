namespace Gvn.GvnFramework.Core.Exceptions;

public class ConflictException : GvnException
{
    public ConflictException(string message) : base(message) { }

    public ConflictException(string entityName, object key)
        : base($"{entityName} with key '{key}' already exists.") { }
}
