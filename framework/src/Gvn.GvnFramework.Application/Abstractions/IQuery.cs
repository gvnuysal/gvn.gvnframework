using Gvn.GvnFramework.Core.Results;
using MediatR;

namespace Gvn.GvnFramework.Application.Abstractions;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
