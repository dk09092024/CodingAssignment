using MediatR;

namespace Domain.Features.Transaction.ValidateTransaction;

public class ValidateTransactionHandler : IRequestHandler<ValidateTransactionRequest, ValidateTransactionResponse>
{
    public Task<ValidateTransactionResponse> Handle(ValidateTransactionRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}