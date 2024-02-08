using MediatR;
using System.Transactions;

namespace Aurora.Framework.Behavior;

public class UnitOfWorkBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : class
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkBehavior(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (IsNotCommand()) return await next();

        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        var response = await next();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        transactionScope.Complete();

        return response;
    }

    private static bool IsNotCommand()
    {
        return !typeof(TRequest).Name.EndsWith("Command");
    }
}