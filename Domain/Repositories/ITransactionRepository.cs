using Domain.Model;
using Domain.Model.ENUM;

namespace Domain.Repositories;

public interface ITransactionRepository : IRepository<Transaction,Guid>
{
    public Task<Transaction> AddTransactionAsync(Guid accountId, decimal amount,
        TransactionType transactionType, DateTime timeRecived,CancellationToken? cancellationToken = null);
    public Task<TransactionProtokol> GetTransactionProtocolAsync(Guid requestTransactionId
        ,CancellationToken? cancellationToken = null);
    public Task UpdateTransactionStateAsync(TransactionProtokol transactionProtokol, TransactionState state,
        DateTime? timeOfExcecution = null, decimal? balanceBeforeExecution = null,
        decimal? balanceAfterExcecution = null,CancellationToken? cancellationToken = null);
    public Task<List<TransactionProtokol>> GetAllTransactionsAsync(Guid[] requestValidatedTransactionIds, 
        Guid? onlyForAccountId, TransactionState? onlyInState,CancellationToken? cancellationToken = null);
    public Task<List<TransactionProtokol>> GetAllRecivedTransactionsAsync(int batchSizeValidation,
        CancellationToken? cancellationToken = null);
    public Task<List<TransactionProtokol>> GetAllValidTransactionsAsync(int batchSizeExecution,
        CancellationToken? cancellationToken = null);
}