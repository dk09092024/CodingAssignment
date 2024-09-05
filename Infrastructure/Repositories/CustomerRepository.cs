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

    public async Task AddAsync(Customer entity)
    {
        _repositoryContext.Customers.Add(entity);
        await _repositoryContext.SaveChangesAsync();
    }

    public async Task<Customer> GetCustomerInformationAsync(Guid requestCustomerId, bool? isIncludingAccounts = false)
    {
        var query = _repositoryContext.Customers.AsQueryable();
        if (isIncludingAccounts ?? false)
        {
            query = query.Include(c => c.Accounts)
                .ThenInclude(a=>a.TransactionHistory)
                .ThenInclude(tp => tp.Transaction);
        }
        return await query.SingleAsync(s=>s.Id == requestCustomerId);
    }
}