using Domain.Model;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AccountRepository : IAccountRepository 
{
    private ApplicationDatabaseContext _repositoryContext;

    public AccountRepository(ApplicationDatabaseContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public async Task<Account> OpenNewAccountAsync(Guid customerId,CancellationToken? cancellationToken = null)
    {
        var customer = await _repositoryContext.Customers.SingleAsync(c => c.Id==customerId,cancellationToken ?? CancellationToken.None);
        var account = new Account
        {
            Balance = 0,
            TransactionHistory = new (),
            Customer = customer,
            Id = Guid.NewGuid(),
            TimeCreated = DateTime.Now
        };
        await AddAccountAsync(account);
        return account;
    }

    private async Task AddAccountAsync(Account account,CancellationToken? cancellationToken = null)
    {
        _repositoryContext.Accounts.Add(account);
        await _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }

    public async Task<Account> GetAccountAsync(Guid accountId, bool? isIncludingTransactionHistory,CancellationToken? cancellationToken = null)
    {
        var query = _repositoryContext.Accounts.AsQueryable();
        if (isIncludingTransactionHistory ?? false)
        {
            query = query.Include(a => a.TransactionHistory)
                .ThenInclude(tp => tp.Transaction);
        }
        return await query.SingleAsync(s => s.Id == accountId,cancellationToken ?? CancellationToken.None);
    }

    public async Task<bool> ExistAccountAsync(Guid accountId,CancellationToken? cancellationToken = null)
    {
        return await _repositoryContext.Accounts.AnyAsync(a => a.Id == accountId,cancellationToken ?? CancellationToken.None);
    }

    public Task UpdateAccountAsync(Account account,CancellationToken? cancellationToken = null)
    {
        account.LastModified= DateTime.Now;
        _repositoryContext.Accounts.Update(account);
        return _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }
}