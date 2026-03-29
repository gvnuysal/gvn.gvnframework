namespace Gvn.GvnFramework.Core.Exceptions;

public class UnauthorizedException : GvnException
{
    public UnauthorizedException() : base("You are not authorized to perform this action.") { }

    public UnauthorizedException(string message) : base(message) { }
}
