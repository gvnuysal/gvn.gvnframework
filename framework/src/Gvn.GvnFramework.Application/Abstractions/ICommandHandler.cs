using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Abstractions;

/// <summary>
/// Handler interface for commands that do not return a typed response value.
/// Implement this interface to handle an <see cref="ICommand"/>.
/// </summary>
/// <typeparam name="TCommand">The command type to handle.</typeparam>
public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

/// <summary>
/// Handler interface for commands that return a typed response value.
/// Implement this interface to handle an <see cref="ICommand{TResponse}"/>.
/// </summary>
/// <typeparam name="TCommand">The command type to handle.</typeparam>
/// <typeparam name="TResponse">The type of the value returned on success.</typeparam>
public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;
