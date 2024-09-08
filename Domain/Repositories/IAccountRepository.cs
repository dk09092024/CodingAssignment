using Domain.Model;

namespace Domain.Repositories;

public interface IAccountRepository : IRepository<Account,Guid>
{
    public Task<Account> OpenNewAccountAsync(Guid customerId,CancellationToken? cancellationToken = null);
    public Task<Account> GetAccountAsync(Guid accountId, bool? isIncludingTransactionHistory = false,CancellationToken? cancellationToken = null);
    public Task<bool> ExistAccountAsync(Guid accountId,CancellationToken? cancellationToken = null);
    public Task UpdateAccountAsync(Account account,CancellationToken? cancellationToken = null);
}