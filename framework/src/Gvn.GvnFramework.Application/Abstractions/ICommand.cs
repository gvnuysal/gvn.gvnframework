using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Abstractions;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;
