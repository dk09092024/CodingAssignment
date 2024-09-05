using Domain.Model.ENUM;
using MediatR;

namespace Domain.Features.Transaction.ReciveTransaction;

public record ReciveTransactionRequest(decimal Amount, Guid RecivingAccountId, TransactionType Type) : IRequest<ReciveTransactionResponse>; 