using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Abstractions;

/// <summary>
/// Marker interface for read-side CQRS queries.
/// Implementing this interface signals that the operation retrieves data and returns a
/// <see cref="Result{TResponse}"/> wrapping the query result.
/// </summary>
/// <typeparam name="TResponse">The type of the value returned on success.</typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
