using MediatR;

namespace Domain.Features.Transaction.ProcessTransactions;

public abstract record BaseProcessTransactionsRequest() : IRequest<ProcessTransactionsResponse>;

public record ProcessTransactionsRequest() : BaseProcessTransactionsRequest();