namespace Gvn.GvnFramework.Core.Results;

/// <summary>
/// Represents the outcome of an operation that does not return a value.
/// Carries a success flag and a list of errors when the operation fails.
/// </summary>
public class Result
{
    /// <summary>
    /// Gets a value indicating whether the operation succeeded.
    /// </summary>
    public bool Succeeded { get; protected set; }

    /// <summary>
    /// Gets the list of errors that occurred. Empty when the operation succeeded.
    /// </summary>
    public List<Error> Errors { get; protected set; } = [];

    /// <summary>
    /// Gets the first error in the list, or <c>null</c> if there are no errors.
    /// </summary>
    public Error? FirstError => Errors.Count > 0 ? Errors[0] : null;

    /// <summary>
    /// Creates a successful result.
    /// </summary>
    /// <returns>A <see cref="Result"/> with <see cref="Succeeded"/> set to <c>true</c>.</returns>
    public static Result Ok() => new() { Succeeded = true };

    /// <summary>
    /// Creates a failed result with the provided errors.
    /// </summary>
    /// <param name="errors">One or more errors that describe the failure.</param>
    /// <returns>A <see cref="Result"/> with <see cref="Succeeded"/> set to <c>false</c>.</returns>
    public static Result Fail(params Error[] errors)
        => new() { Succeeded = false, Errors = errors.ToList() };

    /// <summary>
    /// Creates a failed result from a collection of errors.
    /// </summary>
    /// <param name="errors">A collection of errors that describe the failure.</param>
    /// <returns>A <see cref="Result"/> with <see cref="Succeeded"/> set to <c>false</c>.</returns>
    public static Result Fail(IEnumerable<Error> errors)
        => new() { Succeeded = false, Errors = errors.ToList() };

    /// <summary>
    /// Executes one of the two provided functions depending on whether the result succeeded or failed.
    /// </summary>
    /// <typeparam name="TResult">The return type of the match functions.</typeparam>
    /// <param name="onSuccess">The function to invoke when the result succeeded.</param>
    /// <param name="onFailure">The function to invoke with the error list when the result failed.</param>
    /// <returns>The value returned by whichever function was invoked.</returns>
    public TResult Match<TResult>(Func<TResult> onSuccess, Func<List<Error>, TResult> onFailure)
        => Succeeded ? onSuccess() : onFailure(Errors);
}

/// <summary>
/// Represents the outcome of an operation that returns a value of type <typeparamref name="T"/>.
/// Carries a success flag, the resulting data on success, and a list of errors on failure.
/// </summary>
/// <typeparam name="T">The type of the value returned on success.</typeparam>
public sealed class Result<T> : Result
{
    /// <summary>
    /// Gets the data returned by the operation. <c>null</c> when the result failed.
    /// </summary>
    public T? Data { get; private set; }

    /// <summary>
    /// Creates a successful result carrying the provided data.
    /// </summary>
    /// <param name="data">The value produced by the operation.</param>
    /// <returns>A <see cref="Result{T}"/> with <see cref="Result.Succeeded"/> set to <c>true</c>.</returns>
    public static Result<T> Ok(T data) => new() { Succeeded = true, Data = data };

    /// <summary>
    /// Creates a failed result with the provided errors.
    /// </summary>
    /// <param name="errors">One or more errors that describe the failure.</param>
    /// <returns>A <see cref="Result{T}"/> with <see cref="Result.Succeeded"/> set to <c>false</c>.</returns>
    public new static Result<T> Fail(params Error[] errors)
        => new() { Succeeded = false, Errors = errors.ToList() };

    /// <summary>
    /// Creates a failed result from a collection of errors.
    /// </summary>
    /// <param name="errors">A collection of errors that describe the failure.</param>
    /// <returns>A <see cref="Result{T}"/> with <see cref="Result.Succeeded"/> set to <c>false</c>.</returns>
    public new static Result<T> Fail(IEnumerable<Error> errors)
        => new() { Succeeded = false, Errors = errors.ToList() };

    /// <summary>
    /// Executes one of the two provided functions depending on whether the result succeeded or failed.
    /// </summary>
    /// <typeparam name="TResult">The return type of the match functions.</typeparam>
    /// <param name="onSuccess">The function to invoke with <see cref="Data"/> when the result succeeded.</param>
    /// <param name="onFailure">The function to invoke with the error list when the result failed.</param>
    /// <returns>The value returned by whichever function was invoked.</returns>
    public TResult Match<TResult>(Func<T, TResult> onSuccess, Func<List<Error>, TResult> onFailure)
        => Succeeded && Data is not null ? onSuccess(Data) : onFailure(Errors);
}
