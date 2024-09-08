using Domain.Model;

namespace Domain.Repositories;

public interface IAccountRepository : IRepository<Account,Guid>
{
    public Task<Account> OpenNewAccountAsync(Guid customerId);
    public Task<Account> GetAccountAsync(Guid accountId, bool? isIncludingTransactionHistory = false);
    public Task<bool> ExistAccountAsync(Guid accountId);
    public Task UpdateAccountAsync(Account account);
}