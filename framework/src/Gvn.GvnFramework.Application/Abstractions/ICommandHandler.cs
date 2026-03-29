using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Abstractions;

public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;
