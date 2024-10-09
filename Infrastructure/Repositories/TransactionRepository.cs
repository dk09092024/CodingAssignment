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
        TransactionType transactionType, DateTime timeReceived,CancellationToken? cancellationToken = null)
    {
        var account = await _repositoryContext.Accounts.SingleAsync(a => a.Id == accountId);
        var transaction = new Transaction
        {
            Amount = amount,
            Id = Guid.NewGuid(),
            TargetAccount = account,
            Type = transactionType,
            TimeReceived = timeReceived,
            TargetAccountId = accountId
        };
        _repositoryContext.Transactions.Add(transaction);
        await _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        var transactionHistory = new TransactionProtocol
        {
            AccountId = accountId,
            Account = account,
            Transaction = transaction,
            State = TransactionState.Received,
            Id = Guid.NewGuid(),
            TimeCreated = timeReceived,
            TransactionId = transaction.Id
        };
        _repositoryContext.TransactionProtocols.Add(transactionHistory);
        await _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        return transaction;
    }

    public async Task<TransactionProtocol> GetTransactionProtocolAsync(Guid requestTransactionId,
        CancellationToken? cancellationToken = null)
    {
        return await _repositoryContext.TransactionProtocols.Include(tp=> tp.Transaction)
            .SingleAsync(t => t.TransactionId == requestTransactionId, 
                cancellationToken ?? CancellationToken.None);
    }

    public async Task UpdateTransactionStateAsync(TransactionProtocol transactionProtocol, TransactionState state,
        DateTime? timeOfExcecution = null, decimal? balanceBeforeExecution = null,
        decimal? balanceAfterExcecution = null,CancellationToken? cancellationToken = null)
    {
        transactionProtocol.State = state;
        transactionProtocol.TimeOfExecution = timeOfExcecution ?? transactionProtocol.TimeOfExecution;
        transactionProtocol.BalanceBefore = balanceBeforeExecution ?? transactionProtocol.BalanceBefore;
        transactionProtocol.BalanceAfter = balanceAfterExcecution ?? transactionProtocol.BalanceAfter;
        await _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }

    public async Task<List<TransactionProtocol>> GetAllTransactionsAsync(Guid[] requestValidatedTransactionIds, 
        Guid? onlyForAccountId, TransactionState? onlyInState, CancellationToken? cancellationToken = null)
    {
        var protocols =  await _repositoryContext.TransactionProtocols
            .Include(protocol => protocol.Transaction)
            .Where(protocol => requestValidatedTransactionIds.Any(id => id == protocol.TransactionId))
            .ToListAsync(cancellationToken ?? CancellationToken.None);
        if(onlyForAccountId.HasValue && protocols.Any(tp => tp.AccountId != onlyForAccountId))
            throw new Exception("Includes transactions from other accounts");
        if(onlyInState.HasValue && protocols.Any(tp => tp.State != onlyInState))
            throw new Exception("Includes transactions in other states");
        return protocols;

    }

    public async Task<List<TransactionProtocol>> GetAllReceivedTransactionsAsync(int batchSizeValidation,
        CancellationToken? cancellationToken = null)
    {
        return await _repositoryContext.TransactionProtocols.Where(tp => tp.State == TransactionState.Received)
            .OrderBy(tp => tp.TimeCreated)
            .Take(batchSizeValidation)
            .ToListAsync(cancellationToken ?? CancellationToken.None);
    }

    public async Task<List<TransactionProtocol>> GetAllValidTransactionsAsync(int batchSizeExecution, 
        CancellationToken? cancellationToken = null)
    {
        return await _repositoryContext.TransactionProtocols.Where(tp => tp.State == TransactionState.Valid)
            .OrderBy(tp => tp.TimeCreated)
            .Take(batchSizeExecution)
            .ToListAsync(cancellationToken ?? CancellationToken.None);
    }
}