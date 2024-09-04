using MediatR;

namespace Domain.Features.Transaction.ValidateTransaction;

public record ValidateTransactionRequest(Guid TransactionId, Guid AccountId) : IRequest<ValidateTransactionResponse>;