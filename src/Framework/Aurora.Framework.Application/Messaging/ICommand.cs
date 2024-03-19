using MediatR;

namespace Aurora.Framework.Application;

public interface ICommand : IRequest<Result>;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>;