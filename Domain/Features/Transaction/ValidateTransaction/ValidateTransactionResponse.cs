using Domain.Model.ENUM;

namespace Domain.Features.Transaction.ValidateTransaction;

public record ValidateTransactionResponse(Guid TransactionId, TransactionState State);