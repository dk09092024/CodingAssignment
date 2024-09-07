using Domain.Model.ENUM;

namespace Domain.Features.Transaction.ExecuteTransactionsForAccount;

public record ExecuteTransactionsForAccountResponse(ExecutedTransaction[] ExecutedTransactions);

public record ExecutedTransaction(Guid TransactionId, TransactionType Type, TransactionState State);
