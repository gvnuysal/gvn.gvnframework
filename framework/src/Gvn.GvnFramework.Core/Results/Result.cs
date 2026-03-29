namespace Gvn.GvnFramework.Core.Results;

public class Result
{
    public bool Succeeded { get; protected set; }
    public List<Error> Errors { get; protected set; } = [];

    public Error? FirstError => Errors.Count > 0 ? Errors[0] : null;

    public static Result Ok() => new() { Succeeded = true };

    public static Result Fail(params Error[] errors)
        => new() { Succeeded = false, Errors = errors.ToList() };

    public static Result Fail(IEnumerable<Error> errors)
        => new() { Succeeded = false, Errors = errors.ToList() };

    public TResult Match<TResult>(Func<TResult> onSuccess, Func<List<Error>, TResult> onFailure)
        => Succeeded ? onSuccess() : onFailure(Errors);
}

public sealed class Result<T> : Result
{
    public T? Data { get; private set; }

    public static Result<T> Ok(T data) => new() { Succeeded = true, Data = data };

    public new static Result<T> Fail(params Error[] errors)
        => new() { Succeeded = false, Errors = errors.ToList() };

    public new static Result<T> Fail(IEnumerable<Error> errors)
        => new() { Succeeded = false, Errors = errors.ToList() };

    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<List<Error>, TResult> onFailure)
        => Succeeded && Data is not null ? onSuccess(Data) : onFailure(Errors);
}