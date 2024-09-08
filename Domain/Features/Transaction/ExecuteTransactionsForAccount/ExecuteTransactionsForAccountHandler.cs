using Domain.Model;
using Domain.Model.ENUM;
using Domain.Repositories;
using MediatR;

namespace Domain.Features.Transaction.ExecuteTransactionsForAccount;

public class ExecuteTransactionsForAccountHandler : IRequestHandler<ExecuteTransactionsForAccountRequest, ExecuteTransactionsForAccountResponse>
{
    private ITransactionRepository _transactionRepository;
    private IAccountRepository _accountRepository;

    public ExecuteTransactionsForAccountHandler(ITransactionRepository transactionRepository, IAccountRepository accountRepository)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
    }

    public async Task<ExecuteTransactionsForAccountResponse> Handle(ExecuteTransactionsForAccountRequest request, CancellationToken cancellationToken)
    {
        var transactionprotokolls = await _transactionRepository.GetAllTransactionsAsync(request.ValidatedTransactionIds,
            request.AccountId,TransactionState.Processing, cancellationToken);
        var account = await _accountRepository.GetAccountAsync(request.AccountId,isIncludingTransactionHistory:false, cancellationToken);
        await ProcessInitalTransactions(account, transactionprotokolls, cancellationToken);
        await ProcessDepositTransactions(account, transactionprotokolls, cancellationToken);
        await ProcessWithdrawalTransactions(account, transactionprotokolls, cancellationToken);
        return new ExecuteTransactionsForAccountResponse(
            transactionprotokolls.Select(protokol => new ExecutedTransaction(protokol.Transaction.Id, protokol.Transaction.Type, protokol.State)).ToArray()
            );
    }
    private async Task ProcessInitalTransactions(Model.Account account, List<TransactionProtokol> transactionprotokolls,
        CancellationToken? cancellationToken)
    {
        foreach (var transactionProtokol in transactionprotokolls.Where(protokol => protokol.Transaction.Type == TransactionType.Initial))
        {
            var balanceBefore = account.Balance;
            account.Balance += transactionProtokol.Transaction.Amount;
            await _accountRepository.UpdateAccountAsync(account, cancellationToken);
            await _transactionRepository.UpdateTransactionStateAsync(transactionProtokol, TransactionState.Completed, DateTime.Now, balanceBefore, account.Balance, cancellationToken);
        }
    }
    private async Task ProcessDepositTransactions(Model.Account account,
        List<TransactionProtokol> transactionprotokolls, CancellationToken? cancellationToken)
    {
        foreach (var transactionProtokol in transactionprotokolls.Where(protokol => protokol.Transaction.Type == TransactionType.Deposit))
        {
            var balanceBefore = account.Balance;
            account.Balance += transactionProtokol.Transaction.Amount;
            await _accountRepository.UpdateAccountAsync(account, cancellationToken);
            await _transactionRepository.UpdateTransactionStateAsync(transactionProtokol, TransactionState.Completed,
                DateTime.Now, balanceBefore, account.Balance, cancellationToken);
        }
    }
    private async Task ProcessWithdrawalTransactions(Model.Account account,
        List<TransactionProtokol> transactionprotokolls, CancellationToken? cancellationToken)
    {
        foreach (var transactionProtokol in transactionprotokolls
                     .Where(protokol => protokol.Transaction.Type == TransactionType.Withdrawal)
                     .OrderBy(protokol => protokol.Transaction.TimeRecived))
        {
            if(account.Balance < transactionProtokol.Transaction.Amount)
            {
                await _transactionRepository.UpdateTransactionStateAsync(transactionProtokol, TransactionState.Failed); ;
            }
            else
            {
                var balanceBefore = account.Balance;
                account.Balance += transactionProtokol.Transaction.Amount;
                await _accountRepository.UpdateAccountAsync(account, cancellationToken);
                await _transactionRepository.UpdateTransactionStateAsync(transactionProtokol, TransactionState.Completed,
                    DateTime.Now, balanceBefore, account.Balance, cancellationToken);
            }
        }
    }
}