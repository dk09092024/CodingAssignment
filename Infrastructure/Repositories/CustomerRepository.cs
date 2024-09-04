using Domain.Model;
using Domain.Repositories;

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

    
}