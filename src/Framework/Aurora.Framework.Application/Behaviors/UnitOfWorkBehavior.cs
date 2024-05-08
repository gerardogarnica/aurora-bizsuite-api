using MediatR;
using System.Transactions;

namespace Aurora.Framework.Application;

internal sealed class UnitOfWorkBehavior<TRequest, TResponse>(
    IUnitOfWork unitOfWork)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (IsNotCommand()) return await next();

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var response = await next();
        await unitOfWork.SaveChangesAsync(cancellationToken);
        transactionScope.Complete();

        return response;
    }

    private static bool IsNotCommand()
    {
        return typeof(TRequest).GetInterface("ICommand") is null
            && typeof(TRequest).GetInterface("ICommand`1") is null;
    }
}