using Domain.Model.ENUM;
using MediatR;

namespace Domain.Features.Transaction.ReciveTransaction;

public record ReciveTransactionRequest(decimal Amount, Guid ToAccountId, TransactionType Type) : IRequest<ReciveTransactionResponse>; 