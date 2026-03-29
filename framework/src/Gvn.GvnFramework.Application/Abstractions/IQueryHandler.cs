using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Abstractions;

/// <summary>
/// Handler interface for read-side CQRS queries.
/// Implement this interface to handle an <see cref="IQuery{TResponse}"/>.
/// </summary>
/// <typeparam name="TQuery">The query type to handle.</typeparam>
/// <typeparam name="TResponse">The type of the value returned on success.</typeparam>
public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
