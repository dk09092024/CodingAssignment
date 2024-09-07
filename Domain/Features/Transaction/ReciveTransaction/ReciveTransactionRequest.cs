using System.ComponentModel;
using Domain.Features.Customer.GetCustomerInformation;
using Domain.Model.ENUM;
using MediatR;

namespace Domain.Features.Transaction.ReciveTransaction;

public abstract record BaseReciveTransactionRequest(decimal Amount, Guid RecivingAccountId, TransactionType Type) : IRequest<ReciveTransactionResponse>; 

public record ReciveTransactionRequest(decimal Amount, Guid RecivingAccountId, TransactionType Type) 
    : BaseReciveTransactionRequest(Amount>0 ? Amount: throw new InvalidEnumArgumentException(), 
        RecivingAccountId,
        Type);