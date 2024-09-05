using Domain.Model.ENUM;
using MediatR;

namespace Domain.Features.Transaction.ExecuteTransaction;

public record ExecuteTransactionsForAccountResponse(ExecutedTransaction[] ExecutedTransactions);

public record ExecutedTransaction(Guid TransactionId, TransactionType Type, TransactionState State);
