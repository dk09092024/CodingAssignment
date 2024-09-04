using MediatR;

namespace Domain.Features.Transaction.ExecuteTransaction;

public class ExecuteTransactionHandler : IRequestHandler<ExecuteTransactionRequest, ExecuteTransactionResponse>
{
    public Task<ExecuteTransactionResponse> Handle(ExecuteTransactionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}