using MediatR;

namespace Domain.Features.Transaction.ExecuteTransaction;

public record ExecuteTransactionsForAccountRequest(Guid AccountId,Guid[] ValidatedTransactionIds ) : IRequest<ExecuteTransactionsForAccountResponse>
{
    
}