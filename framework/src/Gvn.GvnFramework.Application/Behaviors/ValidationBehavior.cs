using FluentValidation;
using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Behaviors;

/// <summary>
/// MediatR pipeline behavior that runs all registered <see cref="IValidator{TRequest}"/> instances
/// before the request handler is invoked. If any validation errors are found, the pipeline
/// is short-circuited and a failed <see cref="Result"/> is returned without calling the handler.
/// </summary>
/// <typeparam name="TRequest">The request type being handled.</typeparam>
/// <typeparam name="TResponse">The response type, which must be a <see cref="Result"/>.</typeparam>
public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
    /// <summary>
    /// Validates the request using all registered validators and either short-circuits with errors
    /// or delegates to the next handler in the pipeline.
    /// </summary>
    /// <param name="request">The incoming request.</param>
    /// <param name="next">The delegate representing the next step in the pipeline.</param>
    /// <param name="cancellationToken">A token to cancel the operation.</param>
    /// <returns>A failed result with validation errors, or the result from the next handler.</returns>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var context = new ValidationContext<TRequest>(request);

        var validationErrors = validators
            .Select(v => v.Validate(context))
            .Where(r => r.Errors.Count > 0)
            .SelectMany(r => r.Errors)
            .Select(e => Error.Validation(e.PropertyName, e.ErrorMessage))
            .ToList();

        if (validationErrors.Count > 0)
        {
            var result = Result.Fail(validationErrors);
            return (TResponse)result;
        }

        return await next();
    }
}
