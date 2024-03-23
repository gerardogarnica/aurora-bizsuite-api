using MediatR;

namespace Aurora.Framework.Application;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;