using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Abstractions;

/// <summary>
/// Marker interface for commands that do not return a typed response value.
/// Implementing this interface signals that the operation is a write-side CQRS command
/// and returns a non-generic <see cref="Result"/>.
/// </summary>
public interface ICommand : IRequest<Result>;

/// <summary>
/// Marker interface for commands that return a typed response value.
/// Implementing this interface signals that the operation is a write-side CQRS command
/// and returns a <see cref="Result{TResponse}"/> wrapping the response.
/// </summary>
/// <typeparam name="TResponse">The type of the value returned on success.</typeparam>
public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
