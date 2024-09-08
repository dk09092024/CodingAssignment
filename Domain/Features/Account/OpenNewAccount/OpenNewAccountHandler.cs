using Domain.Model.ENUM;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Account.OpenNewAccount;

public class OpenNewAccountHandler : IRequestHandler<OpenNewAccountRequest, OpenNewAccountResponse>
{
    private IAccountRepository _accountRepository;
    private ITransactionRepository _transactionRepository;

    public OpenNewAccountHandler(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
    {
        _accountRepository = accountRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<OpenNewAccountResponse> Handle(OpenNewAccountRequest request, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.OpenNewAccountAsync(request.CustomerId);
        if(request.InitialBalance > 0)
        {
            var transaction = await _transactionRepository.AddTransactionAsync(account.Id, request.InitialBalance, TransactionType.Initial, DateTime.Now);
            return new OpenNewAccountResponse(account.Id, transaction.Id);
        }
        return new OpenNewAccountResponse(account.Id);
    }
}