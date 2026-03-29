namespace Gvn.GvnFramework.Core.Exceptions;

public class GvnException : Exception
{
    public GvnException(string message) : base(message) { }

    public GvnException(string message, Exception innerException) : base(message, innerException) { }
}
