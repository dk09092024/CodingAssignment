using MediatR;

namespace Domain.Features.Transaction.ProcessTransactions;

public record ProcessTransactionsRequest(Guid AccountId, Guid[] ValidatedTransactionIds) : IRequest<ProcessTransactionsResult>;