using Domain.Model;
using Domain.Model.ENUM;

namespace Domain.Repositories;

public interface ITransactionRepository : IRepository<Transaction,Guid>
{
    public Task<Transaction> AddTransactionAsync(Guid accountId, decimal amount,
        TransactionType transactionType, DateTime timeReceived,CancellationToken? cancellationToken = null);
    public Task<TransactionProtocol> GetTransactionProtocolAsync(Guid requestTransactionId
        ,CancellationToken? cancellationToken = null);
    public Task UpdateTransactionStateAsync(TransactionProtocol transactionProtocol, TransactionState state,
        DateTime? timeOfExcecution = null, decimal? balanceBeforeExecution = null,
        decimal? balanceAfterExcecution = null,CancellationToken? cancellationToken = null);
    public Task<List<TransactionProtocol>> GetAllTransactionsAsync(Guid[] requestValidatedTransactionIds, 
        Guid? onlyForAccountId, TransactionState? onlyInState,CancellationToken? cancellationToken = null);
    public Task<List<TransactionProtocol>> GetAllReceivedTransactionsAsync(int batchSizeValidation,
        CancellationToken? cancellationToken = null);
    public Task<List<TransactionProtocol>> GetAllValidTransactionsAsync(int batchSizeExecution,
        CancellationToken? cancellationToken = null);
}