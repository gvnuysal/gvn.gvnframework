namespace Gvn.GvnFramework.Core.Results;

public class Result
{
    public bool Succeeded { get; protected set; }
    public List<Error> Errors { get; set; } = [];
    public static Result Ok() => new() { Succeeded = true };
    public static Result Fail(params Error[] errors) => new() { Succeeded = false, Errors = errors.ToList() };
}

public sealed class Result<T> : Result
{
    public T? Data { get; private set; }
    public static Result<T> Ok(T data) => new() { Succeeded = true, Data = data };
    public new static Result<T> Fail(params Error[] errors) => new() { Succeeded = false, Errors = errors.ToList() };
}