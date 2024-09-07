using MediatR;

namespace Domain.Features.Transaction.ValidateTransaction;

public abstract record BaseValidateTransactionRequest(Guid TransactionId) : IRequest<ValidateTransactionResponse>;

public record ValidateTransactionRequest(Guid TransactionId) : BaseValidateTransactionRequest(TransactionId);