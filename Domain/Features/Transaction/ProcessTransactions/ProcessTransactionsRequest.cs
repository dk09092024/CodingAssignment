using MediatR;

namespace Domain.Features.Transaction.ProcessTransactions;

public record ProcessTransactionsRequest() : IRequest<ProcessTransactionsResponse>;