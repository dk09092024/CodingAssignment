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
        var transactionprotocolls = await _transactionRepository.GetAllTransactionsAsync(request.ValidatedTransactionIds,
            request.AccountId,TransactionState.Processing, cancellationToken);
        var account = await _accountRepository.GetAccountAsync(request.AccountId,isIncludingTransactionHistory:false, cancellationToken);
        await ProcessInitalTransactions(account, transactionprotocolls, cancellationToken);
        await ProcessDepositTransactions(account, transactionprotocolls, cancellationToken);
        await ProcessWithdrawalTransactions(account, transactionprotocolls, cancellationToken);
        return new ExecuteTransactionsForAccountResponse(
            transactionprotocolls.Select(protocol => new ExecutedTransaction(protocol.Transaction.Id, protocol.Transaction.Type, protocol.State)).ToArray()
            );
    }
    private async Task ProcessInitalTransactions(Model.Account account, List<TransactionProtocol> transactionprotocolls,
        CancellationToken? cancellationToken)
    {
        foreach (var transactionProtocol in transactionprotocolls.Where(protocol => protocol.Transaction.Type == TransactionType.Initial))
        {
            var balanceBefore = account.Balance;
            account.Balance += transactionProtocol.Transaction.Amount;
            await _accountRepository.UpdateAccountAsync(account, cancellationToken);
            await _transactionRepository.UpdateTransactionStateAsync(transactionProtocol, TransactionState.Completed, DateTime.Now, balanceBefore, account.Balance, cancellationToken);
        }
    }
    private async Task ProcessDepositTransactions(Model.Account account,
        List<TransactionProtocol> transactionprotocolls, CancellationToken? cancellationToken)
    {
        foreach (var transactionProtocol in transactionprotocolls.Where(protocol => protocol.Transaction.Type == TransactionType.Deposit))
        {
            var balanceBefore = account.Balance;
            account.Balance += transactionProtocol.Transaction.Amount;
            await _accountRepository.UpdateAccountAsync(account, cancellationToken);
            await _transactionRepository.UpdateTransactionStateAsync(transactionProtocol, TransactionState.Completed,
                DateTime.Now, balanceBefore, account.Balance, cancellationToken);
        }
    }
    private async Task ProcessWithdrawalTransactions(Model.Account account,
        List<TransactionProtocol> transactionprotocolls, CancellationToken? cancellationToken)
    {
        foreach (var transactionProtocol in transactionprotocolls
                     .Where(protocol => protocol.Transaction.Type == TransactionType.Withdrawal)
                     .OrderBy(protocol => protocol.Transaction.TimeReceived))
        {
            if(account.Balance < transactionProtocol.Transaction.Amount)
            {
                await _transactionRepository.UpdateTransactionStateAsync(transactionProtocol, TransactionState.Failed);
            }
            else
            {
                var balanceBefore = account.Balance;
                account.Balance += transactionProtocol.Transaction.Amount;
                await _accountRepository.UpdateAccountAsync(account, cancellationToken);
                await _transactionRepository.UpdateTransactionStateAsync(transactionProtocol, TransactionState.Completed,
                    DateTime.Now, balanceBefore, account.Balance, cancellationToken);
            }
        }
    }
}