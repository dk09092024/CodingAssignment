using Domain.Repositories;
using MediatR;

namespace Domain.Features.Transaction.ReceiveTransaction;

public class ReceiveTransactionHandler : IRequestHandler<ReceiveTransactionRequest, ReciveTransactionResponse>
{
    private readonly IAccountRepository _accountRepository;
    private readonly ITransactionRepository _transactionRepository;

    public ReceiveTransactionHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<ReciveTransactionResponse> Handle(ReceiveTransactionRequest request, CancellationToken cancellationToken)
    {
        if (await _accountRepository.ExistAccountAsync(request.RecivingAccountId, cancellationToken))
        {
            var transaction = await _transactionRepository.AddTransactionAsync(request.RecivingAccountId, request.Amount, 
                request.Type,DateTime.Now,cancellationToken);
            return new ReciveTransactionResponse(transaction.Id);    
        }

        throw new ArgumentException("");
    }
}