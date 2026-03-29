namespace Gvn.GvnFramework.Core.Exceptions;

public class NotFoundException : GvnException
{
    public NotFoundException(string message) : base(message) { }

    public NotFoundException(string entityName, object key)
        : base($"{entityName} with key '{key}' was not found.") { }
}
