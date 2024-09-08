using Domain.Model;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private ApplicationDatabaseContext _repositoryContext;

    public CustomerRepository(ApplicationDatabaseContext repositoryContext)
    {
        _repositoryContext = repositoryContext;
    }

    public async Task AddAsync(Customer entity,CancellationToken? cancellationToken = null)
    {
        _repositoryContext.Customers.Add(entity);
        await _repositoryContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
    }

    public async Task<Customer> GetCustomerInformationAsync(Guid requestCustomerId, bool? isIncludingAccounts = false,CancellationToken? cancellationToken = null)
    {
        var query = _repositoryContext.Customers.AsQueryable();
        if (isIncludingAccounts ?? false)
        {
            query = query.Include(c => c.Accounts)
                .ThenInclude(a=>a.TransactionHistory)
                .ThenInclude(tp => tp.Transaction);
        }
        return await query.SingleAsync(s=>s.Id == requestCustomerId,cancellationToken ?? CancellationToken.None);
    }
}