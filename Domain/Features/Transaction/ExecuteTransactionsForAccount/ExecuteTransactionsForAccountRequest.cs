using MediatR;

namespace Domain.Features.Transaction.ExecuteTransactionsForAccount;

public abstract record BaseExecuteTransactionsForAccountRequest(Guid AccountId, Guid[] ValidatedTransactionIds)
    : IRequest<ExecuteTransactionsForAccountResponse>;
    
public record ExecuteTransactionsForAccountRequest(Guid AccountId, Guid[] ValidatedTransactionIds) 
    : BaseExecuteTransactionsForAccountRequest(AccountId, 
        ValidatedTransactionIds.Length>0 ? ValidatedTransactionIds 
            : throw new ArgumentOutOfRangeException(nameof(ValidatedTransactionIds))); 