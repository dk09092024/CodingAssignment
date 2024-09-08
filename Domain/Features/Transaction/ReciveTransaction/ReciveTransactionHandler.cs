using Domain.Repositories;
using MediatR;

namespace Domain.Features.Transaction.ReciveTransaction;

public class ReciveTransactionHandler : IRequestHandler<ReciveTransactionRequest, ReciveTransactionResponse>
{
    private IAccountRepository _accountRepository;
    private ITransactionRepository _transactionRepository;

    public ReciveTransactionHandler(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<ReciveTransactionResponse> Handle(ReciveTransactionRequest request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.ExistAccountAsync(request.RecivingAccountId);
        var transaction = await _transactionRepository.AddTransactionAsync(request.RecivingAccountId, request.Amount, request.Type,DateTime.Now);
        return new ReciveTransactionResponse(transaction.Id);
    }
}