using System.ComponentModel;
using Domain.Model.ENUM;
using MediatR;

namespace Domain.Features.Transaction.ReceiveTransaction;

public abstract record BaseReciveTransactionRequest(decimal Amount, Guid RecivingAccountId, TransactionType Type) : IRequest<ReciveTransactionResponse>; 

public record ReceiveTransactionRequest(decimal Amount, Guid RecivingAccountId, TransactionType Type) 
    : BaseReciveTransactionRequest(Amount>0 ? Amount: throw new InvalidEnumArgumentException(), 
        RecivingAccountId,
        Type);