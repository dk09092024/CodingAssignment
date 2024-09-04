using Domain.Model.ENUM;
using MediatR;

namespace Domain.Features.Transaction.ExecuteTransaction;

public record ExecuteTransactionResponse(Guid TransactionId, TransactionState State);