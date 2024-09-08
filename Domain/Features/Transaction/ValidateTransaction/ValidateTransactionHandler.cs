using Domain.Model.ENUM;
using Domain.Repositories;
using Domain.Validators;
using MediatR;

namespace Domain.Features.Transaction.ValidateTransaction;

public class ValidateTransactionHandler : IRequestHandler<ValidateTransactionRequest, ValidateTransactionResponse>
{
    private ITransactionRepository _transactionRepository;
    private TransactionValidator _transactionValidator = new();

    public ValidateTransactionHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<ValidateTransactionResponse> Handle(ValidateTransactionRequest request, CancellationToken cancellationToken)
    {
        var transactionProtokol = await _transactionRepository.GetTransactionProtocolAsync(request.TransactionId,
            cancellationToken);
        var validationResult = await _transactionValidator.ValidateAsync(transactionProtokol.Transaction, cancellationToken);
        await _transactionRepository.UpdateTransactionStateAsync(transactionProtokol, 
            validationResult.IsValid ? TransactionState.Valid : TransactionState.Invalid,
            null,null,null,cancellationToken);
        return new ValidateTransactionResponse(request.TransactionId, 
            validationResult.IsValid ? TransactionState.Valid : TransactionState.Invalid);
    }
}