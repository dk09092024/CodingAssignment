using Domain.Model;
using Domain.Model.ENUM;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TransactionRepository : ITransactionRepository
{
    private ApplicationDatabaseContext _repositoryContext;

    public TransactionRepository(ApplicationDatabaseContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public async Task<Transaction> AddTransactionAsync(Guid accountId, decimal amount,
        TransactionType transactionType, DateTime timeRecived,CancellationToken? cancellationToken = null)
    {
        var account = await _repositoryContext.Accounts.SingleAsync(a => a.Id == accountId);
        var transaction = new Transaction
        {
            Amount = amount,
            Id = Guid.NewGuid(),
            TargetAccount = account,
            Type = transactionType,
            TimeRecived = timeRecived,
            TargetAccountId = accountId
        };
        _repositoryContext.Transactions.Add(transaction);
        await _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        var transactionHistory = new TransactionProtokol
        {
            AccountId = accountId,
            Account = account,
            Transaction = transaction,
            State = TransactionState.Recived,
            Id = Guid.NewGuid(),
            TimeCreated = timeRecived,
            TransactionId = transaction.Id
        };
        _repositoryContext.TransactionProtokols.Add(transactionHistory);
        await _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        return transaction;
    }

    public async Task<TransactionProtokol> GetTransactionProtocolAsync(Guid requestTransactionId,
        CancellationToken? cancellationToken = null)
    {
        return await _repositoryContext.TransactionProtokols.Include(tp=> tp.Transaction)
            .SingleAsync(t => t.TransactionId == requestTransactionId, 
                cancellationToken ?? CancellationToken.None);
    }

    public async Task UpdateTransactionStateAsync(TransactionProtokol transactionProtokol, TransactionState state,
        DateTime? timeOfExcecution = null, decimal? balanceBeforeExecution = null,
        decimal? balanceAfterExcecution = null,CancellationToken? cancellationToken = null)
    {
        transactionProtokol.State = state;
        transactionProtokol.TimeOfExecution = timeOfExcecution ?? transactionProtokol.TimeOfExecution;
        transactionProtokol.BalanceBefore = balanceBeforeExecution ?? transactionProtokol.BalanceBefore;
        transactionProtokol.BalanceAfter = balanceAfterExcecution ?? transactionProtokol.BalanceAfter;
        await _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }

    public async Task<List<TransactionProtokol>> GetAllTransactionsAsync(Guid[] requestValidatedTransactionIds, 
        Guid? onlyForAccountId, TransactionState? onlyInState, CancellationToken? cancellationToken = null)
    {
        var protokols =  await _repositoryContext.TransactionProtokols
            .Include(protokol => protokol.Transaction)
            .Where(protokol => requestValidatedTransactionIds.Any(id => id == protokol.TransactionId))
            .ToListAsync(cancellationToken ?? CancellationToken.None);
        if(onlyForAccountId.HasValue && protokols.Any(tp => tp.AccountId != onlyForAccountId))
            throw new Exception("Includes transactions from other accounts");
        if(onlyInState.HasValue && protokols.Any(tp => tp.State != onlyInState))
            throw new Exception("Includes transactions in other states");
        return protokols;

    }

    public async Task<List<TransactionProtokol>> GetAllRecivedTransactionsAsync(int batchSizeValidation,
        CancellationToken? cancellationToken = null)
    {
        return await _repositoryContext.TransactionProtokols.Where(tp => tp.State == TransactionState.Recived)
            .OrderBy(tp => tp.TimeCreated)
            .Take(batchSizeValidation)
            .ToListAsync(cancellationToken ?? CancellationToken.None);
    }

    public async Task<List<TransactionProtokol>> GetAllValidTransactionsAsync(int batchSizeExecution, 
        CancellationToken? cancellationToken = null)
    {
        return await _repositoryContext.TransactionProtokols.Where(tp => tp.State == TransactionState.Valid)
            .OrderBy(tp => tp.TimeCreated)
            .Take(batchSizeExecution)
            .ToListAsync(cancellationToken ?? CancellationToken.None);
    }
}