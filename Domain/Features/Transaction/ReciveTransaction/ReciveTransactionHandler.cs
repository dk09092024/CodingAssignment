using MediatR;

namespace Domain.Features.Transaction.ReciveTransaction;

public class ReciveTransactionHandler : IRequestHandler<ReciveTransactionRequest, ReciveTransactionResponse>
{
    public Task<ReciveTransactionResponse> Handle(ReciveTransactionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}