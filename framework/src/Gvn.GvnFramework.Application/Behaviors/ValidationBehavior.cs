using FluentValidation;
using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Behaviors;

public sealed class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
    where TResponse : Result
{
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
